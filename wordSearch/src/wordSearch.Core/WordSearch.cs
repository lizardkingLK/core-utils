using wordSearch.Core.Helpers;
using wordSearch.Core.Shared.State;
using static wordSearch.Core.Helpers.ApplicationHelper;

namespace wordSearch.Core;

public static class WordSearch
{
    public static void Search(
        bool isInputRedirected,
        TextReader input,
        string[] args)
    {
        Result<Arguments> arguments = ArgumentHelper.GetArguments(args);
        if (arguments.HasErrors)
        {
            HandleError(arguments.Errors);
        }
        
        

        if (!isInputRedirected)
        {
            SearchWordsFromInput(input, arguments);
        }
        else
        {
            SearchWordsFromKeyboard(arguments);
        }
    }

    public static void SearchWordsFromInput(TextReader input, string[] arguments)
    {
        string xd = input.ReadToEnd();
    }

    public static void SearchWordsFromKeyboard(string[] arguments)
    {
        // Trie trie = new();

        // foreach (var item in FileHelper.ReadAllLines())
        // {

        // }
        // string[] lines = 
    }
}