using wordSearch.Core.Library.NonLinear.Tries;
using static wordSearch.Core.Helpers.ApplicationHelper;
using static wordSearch.Core.Helpers.ConsoleHelper;
using static wordSearch.Core.Helpers.InputHelper;
using static wordSearch.Core.Helpers.PaginationHelper;
using static wordSearch.Core.Shared.Constants;
using static wordSearch.Core.Shared.Events;
using static wordSearch.Core.Shared.Values;

namespace wordSearch.Core.Helpers;

public static class PatternsHelper
{
    private static readonly InputHandler _inputHandler;

    static PatternsHelper()
    {
        _inputHandler = new InputHandler(HandleQuery);
    }

    public static void QueryPatternSuggestions(Trie trie)
    {
        if (IsInputUnavailable())
        {
            HandleError(keyboardUnavailableMessage.Content);
            return;
        }

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
                Paginate(suggestions = [.. QueryPatternSuggestions(trie, query)], key, ref pageIndex);
            }

            previous = query;
        }
    }

    public static IEnumerable<string> QueryPatternSuggestions(Trie trie, string query)
    {
        return trie.Patterns(query);
    }
}