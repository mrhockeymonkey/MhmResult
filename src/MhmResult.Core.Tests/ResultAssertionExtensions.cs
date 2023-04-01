namespace MhmResult.Core.Tests;

public static class ResultAssertionExtensions
{
    public static void ShouldBeOkResult<TValue, TError>(this Result<TValue, TError> result) where TValue : notnull where TError : notnull
    {
        Assert.True(result.IsOk);
        Assert.False(result.IsFail);
        
        var valueEx = Record.Exception(() => result.Value);
        Assert.Null(valueEx);
        
        Assert.Throws<InvalidOperationException>(() => result.Error);
    }
    
    public static void ShouldBeFailResult<TValue, TError>(this Result<TValue, TError> result) where TValue : notnull where TError : notnull
    {
        Assert.False(result.IsOk);
        Assert.True(result.IsFail);
        
        var valueEx = Record.Exception(() => result.Error);
        Assert.Null(valueEx);
        
        Assert.Throws<InvalidOperationException>(() => result.Value);
    }
}