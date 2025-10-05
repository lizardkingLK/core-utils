using System.Text.RegularExpressions;

namespace passwordGenerator.Core.Shared;

public static partial class Regexes
{
    [GeneratedRegex(@"^--(\w){2,}$")]
    public static partial Regex FullArgumentRegex();

    [GeneratedRegex(@"^-(\w){1,}$")]
    public static partial Regex PrefixedArgumentRegex();
    [GeneratedRegex(@"^[yYnN]$")]
    public static partial Regex YesNoInputRegex();
}