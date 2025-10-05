namespace passwordGenerator.Core.Abstractions;

public record View
{
    public virtual string? Message { get; }
    public virtual string? Error { get; }
    public virtual string? Data { get; }
}