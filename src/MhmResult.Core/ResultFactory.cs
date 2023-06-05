namespace MhmResult;

public static class Result
{
    // default use case is to use ErrorMessage as the error type...
    public static Result<TValue, ErrorMessage> Ok<TValue>(TValue value) 
        where TValue : notnull 
        => new(value);
    
    public static Result<TValue, ErrorMessage> Error<TValue>(ErrorMessage error) 
        where TValue : notnull 
        => new(error);
    
    public static Result<TValue, ErrorMessage> Error<TValue>(string message) 
        where TValue : notnull 
        => new(new ErrorMessage(message));
    
    // ... but anything can be used as an error for convenience
    public static Result<TValue, TError> Ok<TValue, TError>(TValue value) 
        where TValue : notnull
        where TError : notnull
        => new(value);
    
    public static Result<TValue, TError> Error<TValue, TError>(TError error) 
        where TValue : notnull
        where TError : notnull
        => new(error);
}