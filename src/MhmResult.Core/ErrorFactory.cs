namespace MhmResult;

public static class Error
{
    public static Error<Exception> From(Exception exception) => new(exception);
    
    public static Error<string> From(string errorMessage) => new(errorMessage);
    
    public static Error<int> From(int returnCode) => new(returnCode);
    
    public static Error<TError> From<TError>(TError error) where TError : notnull => new(error);
}