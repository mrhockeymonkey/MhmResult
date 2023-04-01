namespace MhmResult;

public static class Result
{
    // default use case is to use Exception as the error type...
    public static Result<TValue, Exception> Ok<TValue>(TValue value) 
        where TValue : notnull 
        => new(value);
    
    public static Result<TValue, Exception> Fail<TValue>(Exception exception) 
        where TValue : notnull 
        => new(new Error<Exception>(exception));
    
    // ... but anything can be used as an error for convenience
    public static Result<TValue, TError> Ok<TValue, TError>(TValue value) 
        where TValue : notnull
        where TError : notnull
        => new(value);
    
    public static Result<TValue, TError> Fail<TValue, TError>(TError error) 
        where TValue : notnull
        where TError : notnull
        => new(new Error<TError>(error));
}