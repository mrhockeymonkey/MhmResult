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
        if (result.IsError)
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

    static Result<string, ErrorMessage> GetStringFromBadApi()
    {
        try
        {
            var str = BadMethod();
            return Result.Ok(str);
        }
        catch (Exception ex)
        {
            return Result.Error<string>(ex.Message);
        }
    }
    
    static async Task<Result<string, ErrorMessage>> GetStringFromBadApiAsync()
    {
        try
        {
            await Task.Delay(200);
            var str = BadMethod();
            return Result.Ok(str);
        }
        catch (Exception ex)
        {
            return Result.Error<string>(ex.Message);
        }
    }

    static string BadMethod() => throw new HttpRequestException();
}