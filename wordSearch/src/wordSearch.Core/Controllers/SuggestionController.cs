using wordSearch.Core.Abstractions;
using wordSearch.Core.Enums;
using wordSearch.Core.Library.NonLinear.HashMaps;
using wordSearch.Core.Library.NonLinear.Tries;
using wordSearch.Core.Shared.State;
using static wordSearch.Core.Helpers.ApplicationHelper;
using static wordSearch.Core.Helpers.OutputHelper;
using static wordSearch.Core.Helpers.PathHelper;
using static wordSearch.Core.Helpers.TrieHelper;

namespace wordSearch.Core.Controllers;

public record SuggestionController(
    HashMap<ArgumentTypeEnum, object> Arguments) : Controller(Arguments)
{
    public override Result<string> Execute()
    {
        if (Console.IsInputRedirected)
        {
            return SearchWordsFromInput(Console.In);
        }
        else
        {
            return SearchWordsFromPath();
        }
    }

    public Result<string> SearchWordsFromInput(TextReader inputReader)
    {
        Trie trie = GetTrieFromInput(inputReader);
        if (IsValidQuery(out string query))
        {
            OutputSuggestions(QuerySuggestions(trie, query));
        }
        else
        {
            QuerySuggestions(trie);
        }

        return new(string.Empty);
    }

    public Result<string> SearchWordsFromPath()
    {
        if (!IsValidInputFilePath(Arguments[ArgumentTypeEnum.InputPath], out string inputPath))
        {
            HandleError("error. cannot read file. invalid input path given");
        }

        Trie trie = GetTrieFromInputPath(inputPath);
        if (IsValidQuery(out string query))
        {
            OutputSuggestions(QuerySuggestions(trie, query));
        }
        else
        {
            QuerySuggestions(trie);
        }

        return new(string.Empty);
    }

    private void OutputSuggestions(IEnumerable<string> suggestions)
    {
        _ = Arguments.TryGetValue(ArgumentTypeEnum.Count, out object? countObject);
        if (Arguments.TryGetValue(ArgumentTypeEnum.OutputPath, out object? outputPathObject))
        {
            OutputToFile(suggestions, countObject, outputPathObject);
        }
        else
        {
            OutputToConsole(suggestions, countObject, Console.IsOutputRedirected);
        }
    }

    private bool IsValidQuery(out string query)
    {
        query = string.Empty;
        if (Arguments.TryGetValue(
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