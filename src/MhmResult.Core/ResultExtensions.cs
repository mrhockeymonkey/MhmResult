using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;

namespace MhmResult;

public static class ResultExtensions
{
    // TODO is this useful? should a null parameter throw or return Error?
    // public static Result<TValue, TError> ToResult<TValue, TError>(this TValue value)
    //     where TValue : notnull
    //     where TError : notnull
    //     => new(value);

    public static Result<TResult, TError> Map<TValue, TError, TResult>(this Result<TValue, TError> result, Func<TValue, TResult> func) 
        where TValue : notnull 
        where TError : notnull 
        where TResult : notnull =>
        result switch
        {
            { IsOk: true } => new Result<TResult, TError>(func(result.Value)),
            { IsOk: false } => new Result<TResult, TError>(result.Error), // TODO performance?
        };
    
    public static Result<TResult, TError> Map<TValue, TError, TResult>(this Result<TValue, TError> result, Func<TValue, TResult?> func, TResult otherwise) 
        where TValue : notnull 
        where TError : notnull 
        where TResult : notnull
    
    {
        if (!result.IsOk)
        {
            return new Result<TResult, TError>(result.Error); // TODO performance?
        }
    
        var newValue = func(result.Value);
        return newValue switch
        {
            null => new Result<TResult, TError>(otherwise),
            _ => new Result<TResult, TError>(newValue)
        };
    }

    public static Result<TResult, TError> Bind<TValue, TError, TResult>(this Result<TValue, TError> result, Func<TValue, Result<TResult, TError>> func)
        where TValue : notnull
        where TError : notnull
        where TResult : notnull =>
        result switch
        {
            { IsOk: true } => func(result.Value),
            { IsOk: false } => new Result<TResult, TError>(result.Error), // TODO perf?
        };

    public static Result<IEnumerable<TResult>, TError> Traverse<TValue, TResult, TError>(
        this IEnumerable<TValue> enumerable, 
        Func<TValue, Result<TResult, TError>> func)
        where TValue : notnull
        where TError : notnull
        where TResult : notnull
    {
        throw new NotImplementedException();
    }
}