namespace wordSearch.Core.Helpers;

public static class ConsoleHelper
{
    public static void Write(
        object? content,
        ConsoleColor foregroundColor)
    {
        Console.ForegroundColor = foregroundColor;
        Console.Write(content);
        Console.ResetColor();
    }
}