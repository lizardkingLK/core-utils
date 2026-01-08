using wordSearch.Core.Shared.State;
using static wordSearch.Core.Shared.Constants;

namespace wordSearch.Core.Helpers;

public static class ConsoleHelper
{
    static ConsoleHelper()
    {
        Console.CancelKeyPress += (_, _) =>
        {
            ClearWindow();
            ShowCursor();
        };
    }

    public static void ShowCursor()
    {
        Console.CursorVisible = true;
    }

    public static void HideCurosor()
    {
        Console.CursorVisible = false;
    }

    public static void SetCursor(int y, int x)
    {
        Console.SetCursorPosition(x, y);
    }

    public static void ClearWindow()
    {
        Console.Clear();
    }

    public static void ClearLines(int start, int end = 0)
    {
        if (end == 0)
        {
            end = Console.WindowHeight - 1;
        }

        while (start < end)
        {
            Console.SetCursorPosition(0, start);
            Console.WriteLine(new string(SymbolSpace, Console.WindowWidth));
            start++;
        }
    }

    public static void WriteLine(Message message)
    {
        Console.ForegroundColor = message.ForegroundColor;
        Console.SetCursorPosition(message.X, message.Y);
        Console.WriteLine(message.Content);
        Console.SetCursorPosition(0, 0);
        Console.ResetColor();
    }

    public static void WriteNextLine(Message message)
    {
        Console.ForegroundColor = message.ForegroundColor;
        Console.WriteLine(message.Content);
        Console.ResetColor();
    }

    public static void Write(Message message)
    {
        Console.ForegroundColor = message.ForegroundColor;
        Console.SetCursorPosition(message.X, message.Y);
        Console.Write(message.Content);
        Console.ResetColor();
    }

    public static string? ReadLine(Message message)
    {
        Write(message);

        return Console.ReadLine();
    }

    public static ConsoleKeyInfo ReadKey()
    {
        return Console.ReadKey(true);
    }

    public static void ReadKey(Message message)
    {
        Write(message);

        Console.ReadKey(true);
    }
}