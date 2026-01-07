using wordSearch.Core.Library.NonLinear.Tries;
using wordSearch.Core.Shared.State;
using static wordSearch.Core.Helpers.ConsoleHelper;
using static wordSearch.Core.Shared.Constants;

namespace wordSearch.Core.Helpers;

public static class TrieHelper
{
    private static readonly Message _queryMessage = new()
    {
        Content = "Enter keyword: > ",
        ForegroundColor = ConsoleColor.Green,
    };

    private static readonly Message _pageMessage = new()
    {
        ForegroundColor = ConsoleColor.Green,
        Y = 2,
    };

    private static readonly Message _infoMessage = new()
    {
        Content = "[Esc] - New Query    [<] - Back Page   [>] - Next Page",
        ForegroundColor = ConsoleColor.Cyan,
        Y = 2,
    };

    private static readonly Message _hintMessage = new()
    {
        Content = "info. Queries larger than size 30 are not ideal. Use `grep` instead. Press any key to continue...",
        ForegroundColor = ConsoleColor.DarkYellow,
        Y = 1,
    };

    private static readonly Message _warningMessage = new()
    {
        Content = "info. 0 Results found. Press any key to continue...",
        ForegroundColor = ConsoleColor.Yellow,
        Y = 2,
    };

    private static readonly Message _responseMessage = new()
    {
        Y = 4,
    };

    public static Trie GetTrieFromInput(TextReader inputReader)
    {
        Trie trie = new();

        using TextReader stream = inputReader;

        string? line;
        while (stream.Peek() != -1)
        {
            line = stream.ReadLine();
            if (!string.IsNullOrEmpty(line))
            {
                trie.Insert(line);
            }
        }

        return trie;
    }

    public static Trie GetTrieFromInputPath(string inputPath)
    {
        Trie trie = new();

        foreach (string line in FileHelper.ReadAllLines(inputPath))
        {
            if (!string.IsNullOrEmpty(line))
            {
                trie.Insert(line);
            }
        }

        return trie;
    }

    public static void QuerySuggestions(Trie trie)
    {
        string? query;
        IEnumerable<string> suggestions;
        while (true)
        {
            ClearWindow();
            query = ReadLine(_queryMessage);
            if (query!.Length > 30)
            {
                ReadKey(_hintMessage);
                continue;
            }

            suggestions = QuerySuggestions(trie, query ?? string.Empty);

            HandleSuggestions(suggestions, 0);
        }
    }

    public static IEnumerable<string> QuerySuggestions(Trie trie, string query)
    => trie.Autocomplete(query);

    private static void HandleSuggestions(IEnumerable<string> suggestions, int pageIndex)
    {
        HideCurosr();

        IEnumerable<string> paginated;
        while (true)
        {
            paginated = PaginateSuggestions(suggestions, pageIndex, SizePerPage);
            int continuation = GetNoMatches(paginated, ref pageIndex, out string output);
            if (continuation == -1)
            {
                ReadKey(_warningMessage);
                break;
            }
            else if (continuation == 1)
            {
                continue;
            }

            ClearLines(_responseMessage.Y);

            _responseMessage.Content = output;
            WriteLine(_responseMessage);
            WriteLine(_infoMessage);
            WritePageMessage(pageIndex);

            if (!GetInfoCommand(ref pageIndex))
            {
                break;
            }
        }

        ShowCursor();
    }

    private static void WritePageMessage(int pageIndex)
    {
        _pageMessage.Content = new string(SymbolSpace, _pageMessage.Size);
        WriteLine(_pageMessage);

        _pageMessage.Content = string.Format(PageFormat, pageIndex + 1);
        _pageMessage.X = SpacingInfoPage + ((string)_infoMessage.Content!).Length;
        WriteLine(_pageMessage);
    }

    public static IEnumerable<string> PaginateSuggestions(
        IEnumerable<string> source,
        int pageNo,
        int pageSize) => source
        .Skip(pageNo * pageSize)
        .Take(pageSize);

    private static int GetNoMatches(
        IEnumerable<string> paginated,
        ref int pageIndex,
        out string output)
    {
        output = string.Join(Environment.NewLine, paginated);

        if (output != string.Empty)
        {
            return 0;
        }

        if (pageIndex == 0)
        {
            return -1;
        }

        pageIndex = 0;

        return 1;
    }

    private static bool GetInfoCommand(ref int pageIndex)
    {
        ConsoleKeyInfo consoleKeyInfo;

    readKey:
        consoleKeyInfo = ReadKey();
        if (consoleKeyInfo.Key == ConsoleKey.RightArrow)
        {
            pageIndex++;
        }
        else if (consoleKeyInfo.Key == ConsoleKey.LeftArrow)
        {
            pageIndex--;
        }
        else
        {
            return false;
        }

        if (pageIndex < 0)
        {
            pageIndex = 0;
            goto readKey;
        }

        return true;
    }
}