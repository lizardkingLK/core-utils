using System.Text;
using passwordGenerator.Core.Shared;

namespace passwordGenerator.Core.Builder;

public class PasswordBuilder
{
    private int _count = 10;
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

    public Password Build() => new(
        string.Join(
            null,
            Enumerable.Range(0, _count).Select(_ => _sourceBuilder[Random.Shared.Next(_sourceBuilder.Length)])),
        null,
        null);
}
