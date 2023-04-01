namespace MhmResult;

/// <summary>
/// Represents a failure of any type
/// This allows types other than Exception to be used for a failed result such as string error messages or int return codes
/// </summary>
/// <typeparam name="T"></typeparam>
public readonly struct Error<T> where T : notnull // TODO this could be internal potentially?
{
    public Error(T error)
    {
        Value = error ?? throw new ArgumentNullException(nameof(error));
    }

    public T Value { get; }
}