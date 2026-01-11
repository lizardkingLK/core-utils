using wordSearch.Core.Enums;
using wordSearch.Core.Library.NonLinear.HashMaps;
using wordSearch.Core.Shared.State;

namespace wordSearch.Core.Abstractions;

public abstract record Validator(
    HashMap<ArgumentTypeEnum, object> Map,
    Validator? Next = null)
{
    public abstract Result<Arguments> Validate();
}