using static System.Console;
using static System.ConsoleColor;

namespace passwordGenerator.Core.Utility;

public static class ConsoleUtility
{
    public static void WritePrompt() => Write("> ");

    public static void WriteError(string message)
    {
        ForegroundColor = Red;
        WriteLine(message);
        ResetColor();
    }

    public static void WriteInformation(string message)
    {
        ForegroundColor = Cyan;
        WriteLine(message);
        ResetColor();
    }

    public static void WriteData(string message)
    {
        ForegroundColor = Yellow;
        WriteLine(message);
        ResetColor();
    }

    public static void WriteSuccess(string message)
    {
        ForegroundColor = Green;
        WriteLine(message);
        ResetColor();
    }

    public static string? ReadInput() => ReadLine();
}