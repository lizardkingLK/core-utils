using wordSearch.Core.Abstractions;
using wordSearch.Core.Enums;
using wordSearch.Core.Library.NonLinear.HashMaps;
using wordSearch.Core.Library.NonLinear.Tries;
using wordSearch.Core.Shared.State;
using static wordSearch.Core.Helpers.ApplicationHelper;
using static wordSearch.Core.Helpers.InputHelper;
using static wordSearch.Core.Helpers.OutputHelper;
using static wordSearch.Core.Helpers.PathHelper;
using static wordSearch.Core.Helpers.PatternsHelper;
using static wordSearch.Core.Helpers.TrieHelper;
using static wordSearch.Core.Shared.Constants;

namespace wordSearch.Core.Controllers;

public record PatternController(
    HashMap<ArgumentTypeEnum, object> Arguments) : Controller(Arguments)
{
    public override Result<string> Execute()
    {
        if (Arguments.ContainsKey(ArgumentTypeEnum.InputPath))
        {
            SearchPatternsFromPath();
        }
        else
        {
            SearchPatternsFromResource(DictionaryResource);
        }

        return new(string.Empty);
    }

    private Result<string> SearchPatternsFromPath()
    {
        string inputPath = string.Empty;

        if (!Arguments.TryGetValue(ArgumentTypeEnum.InputPath, out object? inputPathObject)
        || !IsValidInputFilePath(inputPathObject, out inputPath))
        {
            HandleError("error. cannot read file. invalid input path given");
        }

        Trie trie = CreateTrieFromInputPath(inputPath);
        if (IsValidQuery(Arguments[ArgumentTypeEnum.Pattern], out string? pattern))
        {
            OutputSuggestions(Arguments, QueryPatternSuggestions(trie, pattern));
        }
        else
        {
            QueryPatternSuggestions(trie);
        }

        return new(string.Empty);
    }

    private Result<string> SearchPatternsFromResource(string resource)
    {
        Trie trie = CreateTrieFromEmbeddedAsset(resource);
        if (IsValidQuery(Arguments[ArgumentTypeEnum.Pattern], out string? pattern))
        {
            OutputSuggestions(Arguments, QueryPatternSuggestions(trie, pattern));
        }
        else
        {
            QueryPatternSuggestions(trie);
        }

        return new(string.Empty);
    }
}