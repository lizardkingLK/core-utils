using wordSearch.Core.Abstractions;
using wordSearch.Core.Enums;
using wordSearch.Core.Library.NonLinear.HashMaps;
using wordSearch.Core.Shared.State;

namespace wordSearch.Core.Validators;

public record InteractiveValidator(
    HashMap<ArgumentTypeEnum, object> Map,
    Validator? Next = null) : Validator(Map, Next)
{
    public override Result<Arguments> Validate()
    {
        throw new NotImplementedException();
    }
}