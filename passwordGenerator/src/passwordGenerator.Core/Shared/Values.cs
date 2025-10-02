using passwordGenerator.Core.Enums;
using passwordGenerator.Core.Library.DataStructures.NonLinear.HashMap;
using static passwordGenerator.Core.Enums.ArgumentTypeEnum;

namespace passwordGenerator.Core.Shared;

public static class Values
{
    public static readonly HashMap<string, (ArgumentTypeEnum, object, bool)> allArgumentsMap
    = new(
        new("h", (Help, true, false)),
        new("help", (Help, true, false)),
        new("i", (Interactive, true, false)),
        new("interactive", (Interactive, true, false)),
        new("n", (Numeric, true, false)),
        new("numeric", (Numeric, true, false)),
        new("l", (LowerCase, true, false)),
        new("lowercase", (LowerCase, true, false)),
        new("u", (UpperCase, true, false)),
        new("uppercase", (UpperCase, true, false)),
        new("s", (Symbolic, true, false)),
        new("symbolic", (Symbolic, true, false)),
        new("c", (Count, 16, true)),
        new("count", (Count, 16, true))
    );
}