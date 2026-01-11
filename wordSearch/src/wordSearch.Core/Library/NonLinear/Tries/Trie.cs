using wordSearch.Core.Library.Linear.Arrays;

namespace wordSearch.Core.Library.NonLinear.Tries;

public class Trie
{
    private record TrieNode
    {
        public Dictionary<char, TrieNode> CharMap { get; } = [];
        public bool IsEndOfWord { get; set; }
        public int OriginalIndex { get; set; }
    }

    private readonly TrieNode _root;

    private readonly DynamicallyAllocatedArray<string> _lines;

    public Trie()
    {
        _root = new();
        _lines = [];
    }

    public void Insert(string word, int index)
    {
        if (string.IsNullOrEmpty(word))
        {
            throw new ArgumentNullException(nameof(word));
        }

        _lines.Add(word);

        word = word.Trim().ToLowerInvariant();

        TrieNode? current = _root;
        foreach (char letter in word)
        {
            if (!current.CharMap.TryGetValue(letter, out TrieNode? child))
            {
                child = new();
                current.CharMap[letter] = child;
            }

            current = child;
        }

        current.IsEndOfWord = true;
        current.OriginalIndex = index;
    }

    public bool Search(string word)
    {
        if (string.IsNullOrEmpty(word))
        {
            throw new ArgumentNullException(nameof(word));
        }

        word = word.Trim().ToLowerInvariant();

        TrieNode? current = _root;
        foreach (char letter in word)
        {
            if (!current.CharMap.TryGetValue(letter, out TrieNode? child))
            {
                return false;
            }

            current = child;
        }

        return current.IsEndOfWord;
    }

    public bool StartsWith(string prefix)
    {
        if (string.IsNullOrEmpty(prefix))
        {
            throw new ArgumentNullException(nameof(prefix));
        }

        prefix = prefix.Trim().ToLowerInvariant();

        TrieNode? current = _root;
        foreach (char letter in prefix)
        {
            if (!current.CharMap.TryGetValue(letter, out TrieNode? child))
            {
                return false;
            }

            current = child;
        }

        return true;
    }

    public void Delete(string word)
    {
        if (string.IsNullOrEmpty(word))
        {
            throw new ArgumentNullException(nameof(word));
        }

        word = word.Trim().ToLowerInvariant();

        _ = Delete(word, _root, 0);
    }

    private static bool Delete(string word, TrieNode current, int index)
    {
        if (index == word.Length)
        {
            if (!current.IsEndOfWord)
            {
                return false;
            }

            current.IsEndOfWord = false;

            return current.CharMap.Count == 0;
        }

        if (!current.CharMap.TryGetValue(word[index], out TrieNode? childNode))
        {
            return false;
        }

        bool canDeleteChild = Delete(word, childNode, index + 1);
        if (canDeleteChild)
        {
            current.CharMap.Remove(word[index]);
        }

        return !current.IsEndOfWord && current.CharMap.Count == 0;
    }

    public IEnumerable<string> Autocomplete(string prefix)
    {
        prefix = prefix.Trim().ToLowerInvariant();

        if (!IsValidPrefix(prefix, out TrieNode? current))
        {
            yield break;
        }

        foreach (int match in FindMatches(current!))
        {
            yield return _lines[match]!;
        }
    }

    private static IEnumerable<int> FindMatches(TrieNode current)
    {
        if (current.IsEndOfWord)
        {
            yield return current.OriginalIndex;
        }

        foreach ((char letter, TrieNode? child) in current.CharMap)
        {
            foreach (int index in FindMatches(child))
            {
                yield return index;
            }
        }
    }

    private bool IsValidPrefix(string prefix, out TrieNode? current)
    {
        current = _root;
        foreach (char letter in prefix)
        {
            if (!current.CharMap.TryGetValue(letter, out TrieNode? child))
            {
                return false;
            }

            current = child;
        }

        return true;
    }

    public IEnumerable<string> Output() => Autocomplete(string.Empty);

    public int Count(string word) => Autocomplete(word).Count();
}