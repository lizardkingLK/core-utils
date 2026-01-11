namespace wordSearch.Core.Shared;

public static class Events
{
    public delegate string InputHandler(string input, out ConsoleKey key);
}