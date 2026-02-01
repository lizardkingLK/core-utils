using wordSearch.Core.Enums;
using wordSearch.Core.Library.NonLinear.HashMaps;
using wordSearch.Core.Shared.State;
using static wordSearch.Core.Enums.ArgumentTypeEnum;
using static wordSearch.Core.Shared.Constants;

namespace wordSearch.Core.Shared;

public static class Values
{
    public static readonly HashMap<string, (ArgumentTypeEnum, object, bool)> allArgumentsMap
    = new(
        new("h", (Help, true, false)),
        new("help", (Help, true, false)),

        new("v", (ArgumentTypeEnum.Version, true, false)),
        new("version", (ArgumentTypeEnum.Version, true, false)),

        new("q", (Query, string.Empty, true)),
        new("query", (Query, string.Empty, true)),

        new("d", (Dictionary, string.Empty, true)),
        new("dictionary", (Dictionary, string.Empty, true)),

        new("a", (Anagrams, string.Empty, true)),
        new("anagrams", (Anagrams, string.Empty, true)),

        new("p", (Pattern, string.Empty, true)),
        new("pattern", (Pattern, string.Empty, true)),

        new("c", (Count, 1000, true)),
        new("cout", (Count, 1000, true)),

        new("i", (InputPath, string.Empty, true)),
        new("input", (InputPath, string.Empty, true)),

        new("o", (OutputPath, string.Empty, true)),
        new("output", (OutputPath, string.Empty, true))
    );

    public static readonly Message queryMessage = new()
    {
        Content = QueryMessage,
        ForegroundColor = ConsoleColor.Green,
    };

    public static readonly Message pageMessage = new()
    {
        ForegroundColor = ConsoleColor.Green,
        Y = 2,
    };

    public static readonly Message controlMessage = new()
    {
        Content = "[Esc] - New Query    [<] - Back Page   [>] - Next Page",
        ForegroundColor = ConsoleColor.Cyan,
        Y = 2,
    };

    public static readonly Message hintMessage = new()
    {
        Content = "info. Queries larger than size 30 are not ideal. Use `grep` instead",
        ForegroundColor = ConsoleColor.DarkYellow,
        Y = 1,
    };

    public static readonly Message responseMessage = new()
    {
        Y = 4,
    };

    public readonly static Message infoMessage = new()
    {
        Content = "info. only 1000 suggestions were written. use --output flag to get all items",
        ForegroundColor = ConsoleColor.DarkYellow,
        Y = Console.WindowHeight - 2,
    };
}