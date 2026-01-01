using System.Text;
using wordSearch.Core.Abstractions;
using wordSearch.Core.Enums;
using wordSearch.Core.Library.NonLinear.HashMaps;
using wordSearch.Core.Shared.State;

namespace wordSearch.Core.Controllers;

public record SuggestionController(HashMap<ArgumentTypeEnum, object> Arguments) : Controller(Arguments)
{
    public override Result<string> Execute()
    {
        if (Console.IsInputRedirected)
        {
            return SearchWordsFromInput(Console.In);
        }
        else
        {
            return SearchWordsFromKeyboard();
        }
    }

    public Result<string> SearchWordsFromInput(TextReader input)
    {
        using TextReader stream = input;
        
        while (stream.Peek() != -1)
        {
            stream.ReadLine();
        }

        Console.WriteLine(xd);

        return new(string.Empty);
    }

    public Result<string> SearchWordsFromKeyboard()
    {
        if (!Arguments.TryGetValue(ArgumentTypeEnum.InputPath, out object? inputPath))
        {
            return new(null, "error. cannot read file. invalid input path given");
        }

        Console.WriteLine(inputPath);
        // Trie trie = new();

        // foreach (var item in FileHelper.ReadAllLines())
        // {

        // }
        // string[] lines = 

        return new(string.Empty);
    }
}