using wordSearch.Core.Library.NonLinear.Tries;
using static wordSearch.Core.Helpers.ConsoleHelper;
using static wordSearch.Core.Shared.Constants;
using static wordSearch.Core.Shared.Values;

namespace wordSearch.Core.Helpers;

public static class TrieHelper
{
    public static Trie CreateTrieFromInput(TextReader inputReader)
    {
        Trie trie = new();

        string? line;
        int count = 0;
        while (inputReader.Peek() != -1)
        {
            line = inputReader.ReadLine();
            if (!string.IsNullOrEmpty(line))
            {
                trie.Insert(line, count++);
            }
        }

        return trie;
    }

    public static Trie CreateTrieFromInputPath(string inputPath)
    {
        Trie trie = new();

        int count = 0;
        foreach (string line in FileHelper.ReadAllLines(inputPath))
        {
            if (!string.IsNullOrEmpty(line))
            {
                trie.Insert(line, count++);
            }
        }

        return trie;
    }
}