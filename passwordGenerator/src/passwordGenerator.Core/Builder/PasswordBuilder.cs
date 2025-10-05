using System.Text;
using passwordGenerator.Core.Shared;
using static passwordGenerator.Core.Shared.Constants;

namespace passwordGenerator.Core.Builder;

public class PasswordBuilder
{
    private int _count = MinCount;
    private readonly StringBuilder _sourceBuilder = new();

    public void UseNumeric()
    {
        _sourceBuilder.Append(string.Join(null, Enumerable.Range(0, 10)));
    }

    public void UseLowerCase()
    {
        _sourceBuilder.Append(string.Join(null, Enumerable.Range(0, 26).Select(item => (char)('a' + item))));
    }

    public void UseUpperCase()
    {
        _sourceBuilder.Append(string.Join(null, Enumerable.Range(0, 26).Select(item => (char)('A' + item))));
    }

    public void UseSymbols()
    {
        _sourceBuilder.Append(string.Join(null, Enumerable.Range(33, 15).Select(item => (char)item)));
        _sourceBuilder.Append(string.Join(null, Enumerable.Range(58, 7).Select(item => (char)item)));
        _sourceBuilder.Append(string.Join(null, Enumerable.Range(91, 6).Select(item => (char)item)));
        _sourceBuilder.Append(string.Join(null, Enumerable.Range(123, 4).Select(item => (char)item)));
    }

    public void UseCount(int count)
    {
        _count = count;
    }

    public PasswordBuilder UseAll(int count)
    {
        UseSymbols();
        UseUpperCase();
        UseLowerCase();
        UseNumeric();
        UseCount(count);

        return this;
    }

    public Password Build()
    {
        if (_sourceBuilder.Length == 0)
        {
            return new();
        }

        IEnumerable<char> passwordAsArray = Enumerable
        .Range(0, _count)
        .Select(_ =>
        _sourceBuilder[Random.Shared.Next(_sourceBuilder.Length)]);

        return new(string.Join(null, passwordAsArray), null, null);
    }
}
