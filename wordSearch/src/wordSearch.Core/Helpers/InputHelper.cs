using wordSearch.Core.Enums;
using wordSearch.Core.Library.NonLinear.HashMaps;
using static wordSearch.Core.Helpers.ConsoleHelper;
using static wordSearch.Core.Shared.Constants;
using static wordSearch.Core.Shared.Values;

namespace wordSearch.Core.Helpers;

public static class InputHelper
{
    public static string HandleQuery(string input, out ConsoleKey key)
    {
        queryMessage.Content += input;
        ConsoleKeyInfo keyInfo = ReadKey(queryMessage, false);
        queryMessage.Content = QueryMessage;

        key = keyInfo.Key;
        if (key == ConsoleKey.Escape)
        {
            Clear(queryMessage.Y, queryMessage.Size, queryMessage.Size + input.Length - QueryMessage.Length);
            return string.Empty;
        }

        if (key == ConsoleKey.Backspace && input != string.Empty)
        {
            Clear(queryMessage.Y, queryMessage.Size + input.Length - 1, 1);
            return input[..^1];
        }

        char keyChar = keyInfo.KeyChar;
        if (char.IsLetterOrDigit(keyChar)
        || char.IsSymbol(keyChar)
        || char.IsPunctuation(keyChar)
        || char.IsWhiteSpace(keyChar))
        {
            return input + keyChar;
        }

        return input;
    }

    public static bool IsValidQuery(object? queryObject, out string query)
    {
        query = string.Empty;

        if (queryObject is string queryString
        && queryString != DictionarySentinel)
        {
            query = queryString;
            return true;
        }

        return false;
    }

    public static bool IsValidQuery(
        HashMap<ArgumentTypeEnum, object> arguments,
        out string query)
    {
        query = string.Empty;

        if (arguments.TryGetValue(
            ArgumentTypeEnum.Query,
            out object? queryObject)
            && queryObject is string queryString)
        {
            query = queryString;
            return true;
        }

        return false;
    }
}