namespace MhmResult;

public readonly struct ErrorMessage
{
    public ErrorMessage(string error)
    {
        Value = error ?? throw new ArgumentNullException(nameof(error));
    }

    public string Value { get; }
    
    // TODO implicit conversion to string
}