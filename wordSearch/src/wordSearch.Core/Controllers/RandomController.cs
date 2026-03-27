using wordSearch.Core.Abstractions;
using wordSearch.Core.Enums;
using wordSearch.Core.Library.NonLinear.HashMaps;
using wordSearch.Core.Library.NonLinear.Tries;
using wordSearch.Core.Shared.State;
using static wordSearch.Core.Helpers.ApplicationHelper;
using static wordSearch.Core.Helpers.InputHelper;
using static wordSearch.Core.Helpers.OutputHelper;
using static wordSearch.Core.Helpers.QueryHelper;
using static wordSearch.Core.Helpers.TrieHelper;
using static wordSearch.Core.Shared.Constants;

namespace wordSearch.Core.Controllers;

public record RandomController(HashMap<ArgumentTypeEnum, object> Arguments) : Controller(Arguments)
{
    public override Result<string> Execute()
    {
        Trie trie = CreateTrieFromEmbeddedAsset(DictionaryResource);
        if (!IsValidCount(Arguments[ArgumentTypeEnum.Random], out int count))
        {
            HandleError("error. invalid count was given. enter between 1 - 1000");
        }

        OutputSuggestions(Arguments, QueryRandomized(trie, count));

        return new(string.Empty);
    }
}