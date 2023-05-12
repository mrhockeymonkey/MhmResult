using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VerifyCS = MhmResult.Analyzer.Tests.Verifiers.CSharpAnalyzerVerifier<MhmResult.Analyzer.ResultTypeCheckedBeforeAccessAnalyzer>;

namespace MhmResult.Analyzer.Tests
{
    [TestClass]
    public class ResultTypeCheckedBeforeAccessAnalyzer
    {
        [TestMethod]
        public async Task Given_EmptyString_When_Analyzing_Then_NoDiagnosticsReturned()
        {
            await VerifyCS.VerifyAnalyzerAsync(String.Empty);
        }

        [TestMethod]
        public async Task Given_ValueAccessedWithoutChecking_Then_ReturnExpectedDiagnostics()
        {
            var source = File.ReadAllText("TestCode/ValueAccessedWithoutChecks.txt");
            await VerifyCS.VerifyAnalyzerAsync(source);
        }
        
        [TestMethod]
        public async Task Given_ValueAccessedAfterIsFailCheck_Then_ReturnExpectedDiagnostics()
        {
            var source = File.ReadAllText("TestCode/ValueAccessedAfterIsFailCheck.txt");
            await VerifyCS.VerifyAnalyzerAsync(source);
        }
        
        [TestMethod]
        public async Task Given_ValueAccessedAfterIsOKCheck_Then_ReturnExpectedDiagnostics()
        {
            var source = File.ReadAllText("TestCode/ValueAccessedAfterIsOkCheck.txt");
            await VerifyCS.VerifyAnalyzerAsync(source);
        }
        
        [TestMethod]
        public async Task Given_ValueAccessedBeforeIsFailCheck_Then_ReturnExpectedDiagnostics()
        {
            var source = File.ReadAllText("TestCode/ValueAccessedBeforeIsFailCheck.txt");
            await VerifyCS.VerifyAnalyzerAsync(source);
        }
        
        [TestMethod]
        public async Task Given_ValueAccessedBeforeIsOKCheck_Then_ReturnExpectedDiagnostics()
        {
            var source = File.ReadAllText("TestCode/ValueAccessedBeforeIsOKCheck.txt");
            await VerifyCS.VerifyAnalyzerAsync(source);
        }

        [TestMethod]
        public async Task Given_MultipleInvocationNamedValue_When_Analyzing_Then_OnlyAnalyzeResultTypes()
        {
            var source = File.ReadAllText("TestCode/OnlyAnalyzeResultTypes.txt");
// https://stackoverflow.com/questions/74178800/how-to-reference-local-assemblies-in-roslyn-analyzer-tests
// https://stackoverflow.com/questions/65550409/adding-reference-assemblies-to-roslyn-analyzer-code-fix-unit-tests
// https://www.meziantou.net/working-with-types-in-a-roslyn-analyzer.htm
            await VerifyCS.VerifyAnalyzerAsync(source);

        }
    }

}
