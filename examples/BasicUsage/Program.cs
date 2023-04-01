using System;
using System.Threading.Tasks;

namespace BasicUsage;

public static class Program
{
    private static async Task Main(string[] args)
    {
        Console.WriteLine($"----- {nameof(MethodReturnUsage)} -----");
        await MethodReturnUsage.RunAsync();
        Console.WriteLine();
        
        Console.WriteLine($"----- {nameof(ExtensionsUsage)} -----");
        await ExtensionsUsage.RunAsync();
        Console.WriteLine();
    }
}