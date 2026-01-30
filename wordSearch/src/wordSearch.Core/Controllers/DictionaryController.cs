using wordSearch.Core.Abstractions;
using wordSearch.Core.Enums;
using wordSearch.Core.Library.NonLinear.HashMaps;
using wordSearch.Core.Library.NonLinear.Tries;
using wordSearch.Core.Shared.State;
using static wordSearch.Core.Helpers.OutputHelper;
using static wordSearch.Core.Helpers.PathHelper;
using static wordSearch.Core.Helpers.QueryHelper;
using static wordSearch.Core.Helpers.TrieHelper;
using static wordSearch.Core.Shared.Constants;

namespace wordSearch.Core.Controllers;

public record DictionaryController(HashMap<ArgumentTypeEnum, object> Arguments) : Controller(Arguments)
{
    public override Result<string> Execute()
    {
        SearchWordsFromPath(DictionaryPath);

        return new(string.Empty);
    }

    public Result<string> SearchWordsFromPath(string path)
    {
        string filePath = GetAssetPath(path);

        Trie trie = CreateTrieFromInputPath(filePath);
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