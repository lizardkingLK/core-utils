using wordSearch.Core.Enums;
using wordSearch.Core.Library.NonLinear.HashMaps;
using static wordSearch.Core.Enums.ArgumentTypeEnum;

namespace wordSearch.Core.Shared;

public static class Values
{
    public static readonly HashMap<string, (ArgumentTypeEnum, object, bool)> allArgumentsMap
    = new(
        new("h", (Help, true, false)),
        new("help", (Help, true, false)),

        new("q", (Query, string.Empty, true)),
        new("query", (Query, string.Empty, true)),

        new("c", (Count, 1000, true)),
        new("cout", (Count, 1000, true)),

        new("i", (InputPath, string.Empty, true)),
        new("input", (InputPath, string.Empty, true)),

        new("o", (OutputPath, string.Empty, true)),
        new("output", (OutputPath, string.Empty, true))
    );
}