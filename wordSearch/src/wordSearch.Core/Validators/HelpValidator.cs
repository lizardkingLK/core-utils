using wordSearch.Core.Abstractions;
using wordSearch.Core.Enums;
using wordSearch.Core.Library.NonLinear.HashMaps;
using wordSearch.Core.Shared.State;

namespace wordSearch.Core.Validators;

public record HelpValidator(
    HashMap<ArgumentTypeEnum, object> Map,
    Validator? Next = null) : Validator(Map, Next)
{
    public override Result<Arguments> Validate()
    {
        if (Map.TryGetValue(ArgumentTypeEnum.Help, out _))
        {
            return new(new Arguments(Map));
        }

        return new(new(Map));
    }
}