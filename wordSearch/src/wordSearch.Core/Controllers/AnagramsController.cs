using wordSearch.Core.Abstractions;
using wordSearch.Core.Enums;
using wordSearch.Core.Library.NonLinear.HashMaps;
using wordSearch.Core.Library.NonLinear.Tries;
using wordSearch.Core.Shared.State;
using static wordSearch.Core.Helpers.AnagramsHelper;
using static wordSearch.Core.Helpers.ApplicationHelper;
using static wordSearch.Core.Helpers.InputHelper;
using static wordSearch.Core.Helpers.OutputHelper;
using static wordSearch.Core.Helpers.PathHelper;
using static wordSearch.Core.Helpers.TrieHelper;
using static wordSearch.Core.Shared.Constants;

namespace wordSearch.Core.Controllers;

public record AnagramsController(
    HashMap<ArgumentTypeEnum, object> Arguments) : Controller(Arguments)
{
    public override Result<string> Execute()
    {
        if (Arguments.ContainsKey(ArgumentTypeEnum.InputPath))
        {
            SearchAnagramsFromPath();
        }
        else
        {
            SearchAnagramsFromResource(DictionaryResource);
        }

        return new(string.Empty);
    }

    public Result<string> SearchAnagramsFromResource(string resource)
    {
        Trie trie = CreateTrieFromEmbeddedAsset(resource);
        if (IsValidQuery(Arguments[ArgumentTypeEnum.Anagrams], out string? query))
        {
            OutputSuggestions(Arguments, QueryAnagramSuggestions(trie, query));
        }
        else
        {
            QueryAnagramSuggestions(trie);
        }

        return new(string.Empty);
    }

    public Result<string> SearchAnagramsFromPath()
    {
        string inputPath = string.Empty;

        if (!Arguments.TryGetValue(ArgumentTypeEnum.InputPath, out object? inputPathObject)
        || !IsValidInputFilePath(inputPathObject, out inputPath))
        {
            HandleError("error. cannot read file. invalid input path given");
        }

        Trie trie = CreateTrieFromInputPath(inputPath);
        if (IsValidQuery(Arguments[ArgumentTypeEnum.Anagrams], out string? query))
        {
            OutputSuggestions(Arguments, QueryAnagramSuggestions(trie, query));
        }
        else
        {
            QueryAnagramSuggestions(trie);
        }

        return new(string.Empty);
    }
}