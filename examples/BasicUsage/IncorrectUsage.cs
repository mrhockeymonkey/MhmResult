using System;
using System.Threading.Tasks;
using MhmResult;

namespace BasicUsage;

public static class IncorrectUsage
{
    public static Task RunAsync()
    {
        var result1 = Result.Ok("hello");
        Console.WriteLine(result1.Value); // should produce a compiler warning

        var result2 = Result.Ok(1234);
        if (result2.IsOk)
        {
            Console.WriteLine(result2.Value); // should not produce a compiler warning
        }
        
        return Task.CompletedTask;
    }
}