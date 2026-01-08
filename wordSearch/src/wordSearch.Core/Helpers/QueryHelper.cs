using wordSearch.Core.Enums;
using wordSearch.Core.Library.NonLinear.HashMaps;
using wordSearch.Core.Library.NonLinear.Tries;
using static wordSearch.Core.Helpers.ConsoleHelper;
using static wordSearch.Core.Helpers.PaginationHelper;
using static wordSearch.Core.Shared.Values;

namespace wordSearch.Core.Helpers;

public static class QueryHelper
{
    public static void QuerySuggestions(Trie trie)
    {
        ClearWindow();

        string? query;
        while (true)
        {
            query = ReadLine(queryMessage);
            if (query is null || query.Length > 30)
            {
                ReadKey(hintMessage);
                continue;
            }

            Paginate([.. QuerySuggestions(trie, query)], 0);
        }
    }

    public static IEnumerable<string> QuerySuggestions(Trie trie, string query)
    {
        return trie.Autocomplete(query);
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