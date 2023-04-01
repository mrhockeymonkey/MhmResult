namespace MhmResult.Core.Tests;

public class ResultFactoryTests
{
    # region Ok
    
    [Fact]
    public void Given_ValueType_When_OkCalled_Then_ReturnOkResult()
    {
        var value = Guid.NewGuid();
        var result = Result.Ok(value);
        
        result.ShouldBeOkResult();
        Assert.Equal(value, result.Value);
    }
    
    [Fact]
    public void Given_ValueType_When_OkWithErrorTypeCalled_Then_ReturnOkResult()
    {
        var value = Guid.NewGuid();
        var result = Result.Ok<Guid, string>(value);
        
        result.ShouldBeOkResult();
        Assert.Equal(value, result.Value);
    }
    
    [Fact]
    public void Given_ReferenceType_When_OkCalled_Then_ReturnOkResult()
    {
        var value = new List<int>{1, 2, 3};
        var result = Result.Ok(value);
        
        result.ShouldBeOkResult();
        Assert.Same(value, result.Value);
    }    
    
    [Fact]
    public void Given_ReferenceType_When_OkWithErrorTypeCalled_Then_ReturnOkResult()
    {
        var value = new List<int>{1, 2, 3};
        var result = Result.Ok<List<int>, List<string>>(value);
        
        result.ShouldBeOkResult();
        Assert.Same(value, result.Value);
    }
    
    # endregion

    #region Fail

    [Fact]
    public void Given_Exception_When_FailCalled_ReturnFailResult()
    {
        var ex = new Exception();
        var result = Result.Fail<int>(ex);
        
        result.ShouldBeFailResult();
        Assert.Same(ex, result.Error);
    }
    
    [Fact]
    public void Given_CustomErrorType_When_FailCalled_ReturnFailResult()
    {
        var errorMessage = "something went wrong";
        var result = Result.Fail<int, string>(errorMessage);
        
        result.ShouldBeFailResult();
        Assert.Same(errorMessage, result.Error);
    }

    #endregion
}