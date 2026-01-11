namespace wordSearch.Core.Shared.State;

public record Result<T>(T? Data, string? Errors = null)
{
    public bool HasErrors => Errors != null;
    public T Value = Data!;
}