using wordSearch.Core.Library.Linear.Arrays;
using wordSearch.Core.Library.NonLinear.HashMaps;

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

    private const char DuplicateSkipSentinel = '\n';

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

        static IEnumerable<int> FindMatches(TrieNode current)
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

        foreach (int match in FindMatches(current!))
        {
            yield return _lines[match]!;
        }
    }

    public IEnumerable<string> Anagrams(string letters)
    {
        int length = letters.Length;
        char[] inputs = [.. letters.OrderBy(letter => letter)];
        HashMap<char, int> counts = [];
        foreach (char input in inputs)
        {
            if (!counts.TryAdd(input, 1))
            {
                counts[input]++;
            }
        }

        IEnumerable<int> FindAnagrams(
            char letter, int wordIndex, TrieNode current)
        {
            if (wordIndex >= length && current.IsEndOfWord)
            {
                yield return current.OriginalIndex;
            }

            char skipped = DuplicateSkipSentinel;

            for (int i = 0; i < length; i++)
            {
                if (counts[inputs[i]] == 0
                || !current!.CharMap.TryGetValue(inputs[i], out TrieNode? child))
                {
                    continue;
                }

                if (inputs[i] == skipped)
                {
                    continue;
                }

                skipped = inputs[i];

                counts[inputs[i]]--;
                foreach (int index in FindAnagrams(inputs[i], wordIndex + 1, child))
                {
                    yield return index;
                }

                counts[inputs[i]]++;
            }
        }

        foreach (int match in FindAnagrams('\n', 0, _root))
        {
            yield return _lines[match]!;
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