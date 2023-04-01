namespace MhmResult.Core.Tests;

public class ResultTests
{
    # region Constructors
    
    [Fact]
    public void Given_ValueIsValueType_When_CreatingResult_ReturnResult()
    {
        var value = 1;
        var result = new Result<int, Exception>(value);
        
        result.ShouldBeOkResult();
        Assert.Equal(value, result.Value);
    }
    
    [Fact]
    public void Given_ValueIsReferenceType_When_CreatingResult_ReturnResult()
    {
        var value = new List<string>{"string", "string", "string"};
        var result = new Result<List<string>, Exception>(value);
        
        result.ShouldBeOkResult();
        Assert.Same(value, result.Value);
    }
    
    [Fact]
    public void Given_ValueIsValueTypeAndErrorIsValueType_When_CreatingResult_ReturnResult()
    {
        var value = 1;
        var result = new Result<int, int>(value);
        
        result.ShouldBeOkResult();
        Assert.Equal(value, result.Value);
    }
    
    [Fact]
    public void Given_ValueIsReferenceTypeAndErrorIsValueType_When_CreatingResult_ReturnResult()
    {
        var value = new List<string>{"string", "string", "string"};
        var result = new Result<List<string>, int>(value);
        
        result.ShouldBeOkResult();
        Assert.Same(value, result.Value);
    }

    [Fact]
    public void Given_ErrorIsOfReferenceType_When_CreatingResult_ReturnResult()
    {
        var error = new Error<Exception>(new Exception());
        var result = new Result<string, Exception>(error);
        
        result.ShouldBeFailResult();
        Assert.Same(error.Value, result.Error);
    }
    
    [Fact]
    public void Given_ErrorIsOfValueType_When_CreatingResult_ReturnResult()
    {
        var error = new Error<int>(7);
        var result = new Result<string, int>(error);
        
        result.ShouldBeFailResult();
        Assert.Equal(error.Value, result.Error);
    }
    
    [Fact]
    public void Given_NullValue_When_CreatingResult_ThrowArgumentNullException()
    {
        string value = default!;
        Assert.Throws<ArgumentNullException>(() => new Result<string, Exception>(value));
    }
    
    [Fact]
    public void Given_NullException_When_CreatingResult_ThrowArgumentNullException()
    {
        Error<Exception> error = default!;
        Assert.Throws<ArgumentNullException>(() => new Result<string, Exception>(error));
    }
    
    # endregion
    
    # region ValueOrDefault

    [Fact]
    public void Given_OkResult_When_ValueOfDefaultOtherwiseCalled_Then_ReturnValue()
    {
        var result = new Result<int, Exception>(1);
        var value = result.ValueOrDefault(2);
        Assert.Equal(1, value);
    }
    
    [Fact]
    public void Given_FailResult_When_ValueOfDefaultOtherwiseCalled_Then_ReturnOtherwise()
    {
        var result = new Result<int, Exception>(new Error<Exception>(new Exception()));
        var value = result.ValueOrDefault(2);
        Assert.Equal(2, value);
    }
    
    [Fact]
    public void Given_OkResult_When_ValueOfDefaultFuncCalled_Then_ReturnValue()
    {
        var result = new Result<int, Exception>(1);
        var value = result.ValueOrDefault(() => 2);
        Assert.Equal(1, value);
    }
    
    [Fact]
    public void Given_FailResult_When_ValueOfDefaultFuncCalled_Then_ReturnOtherwise()
    {
        var result = new Result<int, Exception>(new Error<Exception>(new Exception()));
        var value = result.ValueOrDefault(() => 2);
        Assert.Equal(2, value);
    }
    
    [Fact]
    public void Given_OkResult_When_ValueOfDefaultCalled_Then_ReturnValue()
    {
        var result = new Result<int, Exception>(1);
        var value = result.ValueOrDefault();
        Assert.Equal(1, value);
    }
    
    [Fact]
    public void Given_FailResult_When_ValueOfDefaultCalled_Then_ReturnDefault()
    {
        var result = new Result<int, Exception>(new Error<Exception>(new Exception()));
        var value = result.ValueOrDefault();
        Assert.Equal(default, value);
    }
    
    # endregion
    
    
}