using System;
using System.Net.Http;
using System.Threading.Tasks;
using MhmResult;

namespace BasicUsage;

public static class MethodReturnUsage
{
    public static async Task RunAsync()
    {
        var result = GetStringFromBadApi();
        if (result.IsFail)
        {
            Console.WriteLine($"Sadly the error was {result.Error.GetType()}");
        }

        var asyncResult = await GetStringFromBadApiAsync();
        Console.WriteLine(asyncResult switch
        {
            {IsOk: true} => "We got the string we wanted",
            _ => $"Sadly the async error was {asyncResult.Error.GetType()}"
        });
    }

    static Result<string, Exception> GetStringFromBadApi()
    {
        try
        {
            var str = BadMethod();
            return Result.Ok(str);
        }
        catch (Exception ex)
        {
            return Result.Fail<string>(ex);
        }
    }
    
    static async Task<Result<string, Exception>> GetStringFromBadApiAsync()
    {
        try
        {
            await Task.Delay(200);
            var str = BadMethod();
            return Result.Ok(str);
        }
        catch (Exception ex)
        {
            return Result.Fail<string>(ex);
        }
    }

    static string BadMethod() => throw new HttpRequestException();
}