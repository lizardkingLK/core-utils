using static passwordGenerator.Core.Utility.ConsoleUtility;

namespace passwordGenerator.Core.Helpers;

public static class ResultHelper
{
    public static void HandleError(string message)
    {
        WriteError(message);
        Environment.Exit(-1);
    }

    public static void HandleInformation(string message)
    {
        WriteInfo(message);
        Environment.Exit(0);
    }

    public static void HandleSuccess(string message)
    {
        WriteSuccess(message);
        Environment.Exit(0);
    }
}