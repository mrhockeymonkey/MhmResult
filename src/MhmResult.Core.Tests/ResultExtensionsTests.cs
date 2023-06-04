namespace MhmResult.Core.Tests;

public class ResultExtensionsTests
{
    # region ToResult

    // [Fact]
    // public void Given_Value_When_ToOkResultCalled_ReturnResult()
    // {
    //     var value = new List<string>{"string", "string", "string"};
    //     var result = value.ToOkResult();
    //     
    //     result.ShouldBeOkResult();
    //     Assert.Equal(value, result.Value);
    // }
    
    // [Fact]
    // public void Given_Value_When_TypedToOkResultCalled_ReturnResult()
    // {
    //     var value = new List<string>{"string", "string", "string"};
    //     var result = value.ToOkResult<List<string>, int>();
    //     
    //     result.ShouldBeOkResult();
    //     Assert.Equal(value, result.Value);
    // }

    // [Fact]
    // public void Given_Exception_When_ToFailResultCalled_ReturnResult()
    // {
    //     var ex = new Exception();
    //     var result = ex.ToFailResult<string>();
    //     
    //     result.ShouldBeFailResult();
    //     Assert.Same(ex, result.Error);
    // }
    
    // [Fact]
    // public void Given_Int_When_ToFailResultCalled_ReturnResult()
    // {
    //     var errorCode = 7;
    //     var result = errorCode.ToFailResult<string, int>();
    //     
    //     result.ShouldBeFailResult();
    //     Assert.Equal(errorCode, result.Error);
    // }
    
    // [Fact]
    // public void Given_String_When_ToFailResultCalled_ReturnResult()
    // {
    //     var msg = "something went wrong";
    //     var result = msg.ToFailResult<string, string>();
    //     
    //     result.ShouldBeFailResult();
    //     Assert.Same(msg, result.Error);
    // }
    
    # endregion
    
    # region Map

    [Fact]
    public void Given_OkResult_When_MapCalled_ReturnMappedOkResult()
    {
        var result = Result.Ok<int, ErrorMessage>(7);
        var mappedResult = result.Map(i => i.ToString());
        
        mappedResult.ShouldBeOkResult();
        Assert.Equal("7", mappedResult.Value);
    }

    [Fact]
    public void Given_ErrorResult_When_MapCalled_ReturnMappedErrorResult()
    {
        var err = new ErrorMessage();
        var result = Result.Error<int, ErrorMessage>(err);
        var mappedResult = result.Map(i => i.ToString());
        
        mappedResult.ShouldBeErrorResult();
        Assert.Equal(err, mappedResult.Error);
    }
    
    [Fact]
    public void Given_OkResultAndMapNotNull_When_MapOtherwiseCalled_ReturnMappedOkResult()
    {
        var result = Result.Ok<int, ErrorMessage>(7);
        var mappedResult = result.Map(i => i.ToString(), "8");
        
        mappedResult.ShouldBeOkResult();
        Assert.Equal("7", mappedResult.Value);
    }
    
    [Fact]
    public void Given_OkResultAndMapNull_When_MapOtherwiseCalled_ReturnMappedOkResult()
    {
        var result = Result.Ok<int, ErrorMessage>(7);
        var mappedResult = result.Map(_ => default, "8");
        
        mappedResult.ShouldBeOkResult();
        Assert.Equal("8", mappedResult.Value);
    }
    
    [Fact]
    public void Given_ErrorResult_When_MapOtherwiseCalled_ReturnMappedFailResult()
    {
        var err = new ErrorMessage();
        var result = Result.Error<int, ErrorMessage>(err);
        var mappedResult = result.Map(i => i.ToString());
        
        mappedResult.ShouldBeErrorResult();
        Assert.Equal(err, mappedResult.Error);
    }
    
    # endregion
    
    # region Bind

    [Fact]
    public void Given_OkResultMapsToOkResult_When_BindCalled_ReturnOkResult()
    {
        var result = Result.Ok<int, Exception>(7);
        var mappedResult = result.Bind(i => new Result<string,Exception>(i.ToString()));
        
        mappedResult.ShouldBeOkResult();
        Assert.Equal("7", mappedResult.Value);
    }
    
    [Fact]
    public void Given_OkResultMapsToErrorResult_When_BindCalled_ReturnErrorResult()
    {
        var err = new ErrorMessage();
        var result = Result.Ok<int, ErrorMessage>(7);
        var mappedResult = result.Bind(_ => new Result<string,ErrorMessage>(err));
        
        mappedResult.ShouldBeErrorResult();
        Assert.Equal(err, mappedResult.Error);
    }

    [Fact]
    public void Given_ErrorResult_When_BindCalled_ReturnFailResult()
    {
        var err = new ErrorMessage();
        var result = Result.Error<int, ErrorMessage>(err);
        var mappedResult = result.Bind(i => new Result<string,ErrorMessage>(i.ToString()));
        
        mappedResult.ShouldBeErrorResult();
        Assert.Equal(err, mappedResult.Error);
    }
    
    # endregion
}