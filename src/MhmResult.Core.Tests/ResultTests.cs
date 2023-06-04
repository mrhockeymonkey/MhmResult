namespace MhmResult.Core.Tests;

public class ResultTests
{
    # region Internal Constructors
    
    [Fact]
    public void Given_ValueIsValueType_When_CreatingResult_ReturnResult()
    {
        var value = 1;
        var result = new Result<int, ErrorMessage>(value);
        
        result.ShouldBeOkResult();
        Assert.Equal(value, result.Value);
    }
    
    [Fact]
    public void Given_ValueIsReferenceType_When_CreatingResult_ReturnResult()
    {
        var value = new List<string>{"string", "string", "string"};
        var result = new Result<List<string>, ErrorMessage>(value);
        
        result.ShouldBeOkResult();
        Assert.Same(value, result.Value);
    }
    
    [Fact]
    public void Given_ValueIsValueTypeAndErrorIsValueType_When_CreatingResult_ReturnResult()
    {
        var value = 1;
        var result = new Result<int, double>(value);
        
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
        var error = new Exception();
        var result = new Result<string, Exception>(error);
        
        result.ShouldBeErrorResult();
        Assert.Same(error, result.Error);
    }
    
    [Fact]
    public void Given_ErrorIsOfValueType_When_CreatingResult_ReturnResult()
    {
        var error = 7;
        var result = new Result<int, double>(7.0);
        
        result.ShouldBeErrorResult();
        Assert.Equal(error, result.Error);
    }
    
    [Fact]
    public void Given_NullValue_When_CreatingResult_ThrowArgumentNullException()
    {
        string value = default!;
        Assert.Throws<ArgumentNullException>(() => new Result<string, ErrorMessage>(value));
    }
    
    [Fact]
    public void Given_NullException_When_CreatingResult_ThrowArgumentNullException()
    {
        Exception error = default!;
        Assert.Throws<ArgumentNullException>(() => new Result<string, Exception>(error));
    }
    
    # endregion
    
    # region ValueOrDefault

    [Fact]
    public void Given_OkResult_When_ValueOfDefaultOtherwiseCalled_Then_ReturnValue()
    {
        var result = new Result<int, ErrorMessage>(1);
        var value = result.ValueOrDefault(2);
        Assert.Equal(1, value);
    }
    
    [Fact]
    public void Given_ErrorResult_When_ValueOfDefaultOtherwiseCalled_Then_ReturnOtherwise()
    {
        var result = new Result<int, ErrorMessage>(new ErrorMessage());
        var value = result.ValueOrDefault(2);
        Assert.Equal(2, value);
    }
    
    [Fact]
    public void Given_OkResult_When_ValueOfDefaultFuncCalled_Then_ReturnValue()
    {
        var result = new Result<int, ErrorMessage>(1);
        var value = result.ValueOrDefault(() => 2);
        Assert.Equal(1, value);
    }
    
    [Fact]
    public void Given_ErrorResult_When_ValueOfDefaultFuncCalled_Then_ReturnOtherwise()
    {
        var result = new Result<int, ErrorMessage>(new ErrorMessage());
        var value = result.ValueOrDefault(() => 2);
        Assert.Equal(2, value);
    }
    
    [Fact]
    public void Given_OkResult_When_ValueOfDefaultCalled_Then_ReturnValue()
    {
        var result = new Result<int, ErrorMessage>(1);
        var value = result.ValueOrDefault();
        Assert.Equal(1, value);
    }
    
    [Fact]
    public void Given_ErrorResult_When_ValueOfDefaultCalled_Then_ReturnDefault()
    {
        var result = new Result<int, ErrorMessage>(new ErrorMessage());
        var value = result.ValueOrDefault();
        Assert.Equal(default, value);
    }
    
    # endregion
}