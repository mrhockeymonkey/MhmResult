using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MhmResult.Core.Tests")]
namespace MhmResult;

public readonly struct Result<TValue, TError>
    where TValue : notnull
    where TError : notnull
{
    private readonly TValue? _value;
    private readonly TError? _error;
    
    internal Result(TValue value)
    {
        _value = value ?? throw new ArgumentNullException(Constants.NullValueMessage, nameof(value));
        _error = default;
        IsOk = true;
    }

    internal Result(TError error)
    {
        _value = default;
        _error = error ?? throw new ArgumentNullException(Constants.NullErrorMessage, nameof(error));
        IsOk = false;
    }

    public bool IsOk { get; } // TODO analyzer for use in branching which can introduce bugs
    public bool IsError => !IsOk;
    
    public TValue Value => IsOk ? _value! : throw new InvalidOperationException("Cannot access value when result is not Ok!");
    public TError Error => IsError ? _error! : throw new InvalidOperationException("Cannot access error when result is not Fail!");
    
    public TValue ValueOrDefault(TValue otherwise) => IsOk ? _value! : otherwise;
    public TValue ValueOrDefault(Func<TValue> otherwise) => IsOk ? _value! : otherwise();
    public TValue? ValueOrDefault() => _value;
    
    public T Match<T>(Func<TValue, T> ok, Func<TError, T> error) =>
        IsOk switch
        {
            true when ok is null => throw new ArgumentNullException(nameof(ok)),
            true => ok(Value),
            false when error is null => throw new ArgumentNullException(nameof(error)),
            false => error(Error)
        };
}