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

    public static void HideCursor()
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

    public static void Clear(int y, int x, int count)
    {
        if (x + count > Console.WindowWidth)
        {
            return;
        }

        int i = 0;
        while (i < count)
        {
            Console.SetCursorPosition(x + i, y);
            Console.WriteLine(new string(SymbolSpace, 1));
            i++;
        }
    }

    public static async Task ClearAsync(
        TimeSpan clearAfter,
        Message message)
    {
        await Task.Delay(clearAfter);
        ClearLines(message.Y, message.Y + 1);
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

    public static void Write(Message message, TimeSpan? clearAfter = null)
    {
        Console.ForegroundColor = message.ForegroundColor;
        Console.SetCursorPosition(message.X, message.Y);
        Console.Write(message.Content);
        Console.ResetColor();
        if (clearAfter.HasValue)
        {
            Task.Run(async () => await ClearAsync(clearAfter.Value, message));
        }
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

    public static ConsoleKeyInfo ReadKey(
        Message message,
        bool shouldHideKey = true)
    {
        Write(message);

        return Console.ReadKey(shouldHideKey);
    }
}