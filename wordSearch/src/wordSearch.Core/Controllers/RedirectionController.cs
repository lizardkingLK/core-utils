using wordSearch.Core.Abstractions;
using wordSearch.Core.Enums;
using wordSearch.Core.Library.NonLinear.HashMaps;
using wordSearch.Core.Library.NonLinear.Tries;
using wordSearch.Core.Shared.State;
using static wordSearch.Core.Helpers.InputHelper;
using static wordSearch.Core.Helpers.OutputHelper;
using static wordSearch.Core.Helpers.QueryHelper;
using static wordSearch.Core.Helpers.TrieHelper;

namespace wordSearch.Core.Controllers;

public record RedirectionController(
    HashMap<ArgumentTypeEnum, object> Arguments) : Controller(Arguments)
{
    public override Result<string> Execute()
    {
        Trie trie = CreateTrieFromInput(Console.In);

        string query = IsValidQuery(Arguments, out query) ? query : string.Empty;

        OutputSuggestions(Arguments, QuerySuggestions(trie, query));

        return new(string.Empty);
    }
}