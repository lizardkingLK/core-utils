using wordSearch.Core.Abstractions;
using wordSearch.Core.Enums;
using wordSearch.Core.Library.NonLinear.HashMaps;
using wordSearch.Core.Library.NonLinear.Tries;
using wordSearch.Core.Shared.State;
using static wordSearch.Core.Helpers.InputHelper;
using static wordSearch.Core.Helpers.OutputHelper;
using static wordSearch.Core.Helpers.QueryHelper;
using static wordSearch.Core.Helpers.TrieHelper;
using static wordSearch.Core.Shared.Constants;

namespace wordSearch.Core.Controllers;

public record DictionaryController(HashMap<ArgumentTypeEnum, object> Arguments) : Controller(Arguments)
{
    public override Result<string> Execute()
    {
        Trie trie = CreateTrieFromEmbeddedAsset(DictionaryResource);
        if (IsValidQuery(Arguments[ArgumentTypeEnum.Dictionary], out string? query))
        {
            OutputSuggestions(Arguments, QuerySuggestions(trie, query));
        }
        else
        {
            QuerySuggestions(trie);
        }

        return new(string.Empty);
    }
}