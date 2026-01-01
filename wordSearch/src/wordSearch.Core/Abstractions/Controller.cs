using wordSearch.Core.Enums;
using wordSearch.Core.Library.NonLinear.HashMaps;
using wordSearch.Core.Shared.State;

namespace wordSearch.Core.Abstractions;

public abstract record Controller(HashMap<ArgumentTypeEnum, object>? ArgumentMap)
{
    public abstract Result<string> Execute();
}