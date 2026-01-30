using wordSearch.Core.Enums;
using wordSearch.Core.Library.NonLinear.HashMaps;
using wordSearch.Core.Library.NonLinear.Tries;
using static wordSearch.Core.Helpers.ConsoleHelper;
using static wordSearch.Core.Helpers.PaginationHelper;
using static wordSearch.Core.Shared.Constants;
using static wordSearch.Core.Shared.Events;
using static wordSearch.Core.Shared.Values;

namespace wordSearch.Core.Helpers;

public static class QueryHelper
{
    private static readonly InputHandler _inputHandler;

    static QueryHelper()
    {
        _inputHandler = new InputHandler(HandleQuery);
    }

    public static void QuerySuggestions(Trie trie)
    {
        HideCursor();
        ClearWindow();

        string previous = string.Empty;
        string query = string.Empty;
        List<string> suggestions = [];
        int pageIndex = 0;
        while (true)
        {
            query = _inputHandler.Invoke(previous, out ConsoleKey key);
            if (string.IsNullOrEmpty(query))
            {
                previous = string.Empty;
                suggestions.Clear();
            }

            if (query.Length > MaxQueryLength)
            {
                Clear(0, queryMessage.Size + MaxQueryLength, 1);
                Write(
                    hintMessage,
                    TimeSpan.FromSeconds(HintDurationSeconds));
                HideCursor();
                continue;
            }

            if (query == previous)
            {
                Paginate(suggestions, key, ref pageIndex);
            }
            else
            {
                pageIndex = 0;
                Paginate(suggestions = [.. QuerySuggestions(trie, query)], key, ref pageIndex);
            }

            previous = query;
        }
    }

    private static string HandleQuery(string input, out ConsoleKey key)
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

    public static IEnumerable<string> QuerySuggestions(Trie trie, string query)
    {
        return trie.Autocomplete(query);
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