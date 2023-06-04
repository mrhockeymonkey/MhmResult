namespace MhmResult.Core.Tests;

public class ResultFactoryTests // TODO remove duplicate tests
{
    # region Factories
    
    [Fact]
    public void Given_ValueIsValueType_When_CreatingResult_ReturnResult()
    {
        var value = 1;
        var result = Result.Ok<int, ErrorMessage>(value);
        
        result.ShouldBeOkResult();
        Assert.Equal(value, result.Value);
    }
    
    [Fact]
    public void Given_ValueIsReferenceType_When_CreatingResult_ReturnResult()
    {
        var value = new List<string>{"string", "string", "string"};
        var result = Result.Ok<List<string>, ErrorMessage>(value);
        
        result.ShouldBeOkResult();
        Assert.Same(value, result.Value);
    }
    
    [Fact]
    public void Given_ValueIsValueTypeAndErrorIsValueType_When_CreatingResult_ReturnResult()
    {
        var value = 1;
        var result = Result.Ok<int, int>(value);
        
        result.ShouldBeOkResult();
        Assert.Equal(value, result.Value);
    }
    
    [Fact]
    public void Given_ValueIsReferenceTypeAndErrorIsValueType_When_CreatingResult_ReturnResult()
    {
        var value = new List<string>{"string", "string", "string"};
        var result = Result.Ok<List<string>, int>(value);
        
        result.ShouldBeOkResult();
        Assert.Same(value, result.Value);
    }

    [Fact]
    public void Given_ErrorIsOfReferenceType_When_CreatingResult_ReturnResult()
    {
        var error = new Exception();
        var result = Result.Error<string, Exception>(error);
        
        result.ShouldBeErrorResult();
        Assert.Same(error, result.Error);
    }
    
    [Fact]
    public void Given_ErrorIsOfValueType_When_CreatingResult_ReturnResult()
    {
        var error = 7;
        var result = Result.Error<int, int>(7);
        
        result.ShouldBeErrorResult();
        Assert.Equal(error, result.Error);
    }
    
    [Fact]
    public void Given_NullValue_When_CreatingResult_ThrowArgumentNullException()
    {
        string value = default!;
        Assert.Throws<ArgumentNullException>(() => Result.Ok<string, ErrorMessage>(value));
    }
    
    [Fact]
    public void Given_Null_When_CreatingResult_ThrowArgumentNullException()
    {
        Exception error = default!;
        Assert.Throws<ArgumentNullException>(() => Result.Error<string, Exception>(error));
    }
    
    # endregion
    
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
        var err = new ErrorMessage("something went wrong");
        var result = Result.Error<int>(err);
        
        result.ShouldBeErrorResult();
        Assert.Equal(err, result.Error);
    }
    
    [Fact]
    public void Given_CustomErrorType_When_FailCalled_ReturnFailResult()
    {
        var err = new Exception("something went wrong");
        var result = Result.Error<int, Exception>(err);
        
        result.ShouldBeErrorResult();
        Assert.Same(err, result.Error);
    }
    
    #endregion
}