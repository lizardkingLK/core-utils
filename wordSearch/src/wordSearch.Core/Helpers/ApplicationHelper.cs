namespace wordSearch.Core.Helpers;

public static class ApplicationHelper
{
    public static void HandleError(object? content)
    {
        ConsoleHelper.Write(content, ConsoleColor.Red);
        Environment.Exit(1);
    }
}