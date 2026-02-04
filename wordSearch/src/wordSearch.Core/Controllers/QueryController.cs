using wordSearch.Core.Abstractions;
using wordSearch.Core.Enums;
using wordSearch.Core.Library.NonLinear.HashMaps;
using wordSearch.Core.Library.NonLinear.Tries;
using wordSearch.Core.Shared.State;
using static wordSearch.Core.Helpers.ApplicationHelper;
using static wordSearch.Core.Helpers.InputHelper;
using static wordSearch.Core.Helpers.OutputHelper;
using static wordSearch.Core.Helpers.PathHelper;
using static wordSearch.Core.Helpers.QueryHelper;
using static wordSearch.Core.Helpers.TrieHelper;

namespace wordSearch.Core.Controllers;

public record QueryController(
    HashMap<ArgumentTypeEnum, object> Arguments) : Controller(Arguments)
{
    public override Result<string> Execute()
    {
        string inputPath = string.Empty;

        if (!Arguments.TryGetValue(ArgumentTypeEnum.InputPath, out object? inputPathObject)
        || !IsValidInputFilePath(inputPathObject, out inputPath))
        {
            HandleError("error. cannot read file. invalid input path given");
        }

        Trie trie = CreateTrieFromInputPath(inputPath);
        if (IsValidQuery(Arguments, out string query))
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