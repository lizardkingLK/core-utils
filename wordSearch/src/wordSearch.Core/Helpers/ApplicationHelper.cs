using wordSearch.Core.Shared.State;

namespace wordSearch.Core.Helpers;

public static class ApplicationHelper
{
    private static readonly Message _errorMessage = new()
    {
        ForegroundColor = ConsoleColor.Red,
        Y = 1,
    };

    public static void HandleError(object? content)
    {
        _errorMessage.Content = content;
        ConsoleHelper.WriteLine(_errorMessage);
        Environment.Exit(1);
    }

    public static void HandleSuccess()
    {
        Environment.Exit(0);
    }
}