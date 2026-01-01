namespace wordSearch.Core.Helpers;

public static class ApplicationHelper
{
    public static void HandleError(object? content)
    {
        ConsoleHelper.WriteLine(content, ConsoleColor.Red);
        Environment.Exit(1);
    }
}