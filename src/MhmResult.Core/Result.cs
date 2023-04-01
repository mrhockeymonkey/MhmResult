namespace MhmResult;

public readonly struct Result<TValue, TError>  
    where TValue : notnull
    where TError : notnull
{
    private readonly TValue? _value;
    private readonly TError? _error;
    
    public Result(TValue value)
    {
        _value = value ?? throw new ArgumentNullException(Constants.NullValueMessage, nameof(value));
        _error = default;
        IsOk = true;
    }

    public Result(Error<TError> error)
    {
        _value = default;
        _error = error.Value ?? throw new ArgumentNullException(Constants.NullErrorMessage, nameof(error));
        IsOk = false;
    }

    public bool IsOk { get; }
    public bool IsFail => !IsOk;
    
    public TValue Value => IsOk ? _value! : throw new InvalidOperationException("Cannot access value when result is not Ok!");
    public TError Error => IsFail ? _error! : throw new InvalidOperationException("Cannot access error when result is not Fail!");
    
    public TValue ValueOrDefault(TValue otherwise) => IsOk ? _value! : otherwise;
    public TValue ValueOrDefault(Func<TValue> otherwise) => IsOk ? _value! : otherwise();
    public TValue? ValueOrDefault() => _value;
}