namespace MhmResult.Core.Tests;

public class ResultExtensionsTests
{
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