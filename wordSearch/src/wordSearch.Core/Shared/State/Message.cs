namespace wordSearch.Core.Shared.State;

public record Message
{
    public object? Content { get; set; }
    public int Y { get; set; } = 0;
    public int X { get; set; } = 0;
    public ConsoleColor ForegroundColor { get; set; } = ConsoleColor.White;
    public ConsoleColor BackgroundColor { get; set; } = ConsoleColor.Black;
    public int Size
    {
        get => (Content as string ?? string.Empty).Length;
    }
}