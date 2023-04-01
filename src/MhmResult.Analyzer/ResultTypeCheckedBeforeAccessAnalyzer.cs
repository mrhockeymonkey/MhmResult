using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace MhmResult.Analyzer
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    [SuppressMessage("MicrosoftCodeAnalysisReleaseTracking", "RS2008:Enable analyzer release tracking")]
    public class ResultTypeCheckedBeforeAccessAnalyzer : DiagnosticAnalyzer
    {
        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();
            //context.RegisterSyntaxNodeAction(Analyze, SyntaxKind.MethodDeclaration);
            context.RegisterSemanticModelAction(Analyze2);
        }

        private void Analyze2(SemanticModelAnalysisContext context)
        {
            var model = context.SemanticModel;
            var memberAccessExpressions = model.SyntaxTree
                .GetRoot()
                .DescendantNodes()
                .OfType<MemberAccessExpressionSyntax>();

            var checkedResults = new HashSet<string>();
            INamedTypeSymbol genericResultType = model.Compilation.GetTypeByMetadataName("MhmResult.Result`2");

            foreach(var memberAccessExpression in memberAccessExpressions)
            {
                // For each access expression Name seems to be the member and expression the object the member belongs to...
                if (memberAccessExpression.Expression is not IdentifierNameSyntax expression) continue;
                
                // we are looking for an ILocalSymbol
                // https://learn.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.ilocalsymbol?view=roslyn-dotnet-4.3.0
                var symbolInfo = model.GetSymbolInfo(expression);
                if (symbolInfo.Symbol is not ILocalSymbol localSymbol) continue;

                var symbolType = (INamedTypeSymbol)localSymbol.Type;
                var isResultType = SymbolEqualityComparer.Default.Equals(symbolType.ConstructedFrom, genericResultType);

                if (!isResultType) continue;
                
                var variableName = expression.Identifier.Text;
                
                if (memberAccessExpression.Name.ToString() == "IsOk" || memberAccessExpression.Name.ToString() == "IsFail")
                    checkedResults.Add(variableName);
                    
                if (memberAccessExpression.Name.ToString() == "Value" && !checkedResults.Contains(variableName))
                {
                    var diagnostic = Diagnostic.Create(Rule, memberAccessExpression.GetLocation());
                    context.ReportDiagnostic(diagnostic);
                }
            }
        }
        
        private static readonly DiagnosticDescriptor Rule = new (
            nameof(ResultTypeCheckedBeforeAccessAnalyzer),
            "Result should be checked before use",
            "Check Result using IsOk or IsFail before accessing value",
            "Usage",
            DiagnosticSeverity.Warning,
            isEnabledByDefault: true);
        
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);
    }
}