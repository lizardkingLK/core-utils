using passwordGenerator.Core.Abstractions;
using passwordGenerator.Core.Enums;
using passwordGenerator.Core.Library.DataStructures.NonLinear.HashMap;
using passwordGenerator.Core.Shared;

namespace passwordGenerator.Core.Controllers;

public record InteractiveController(HashMap<ArgumentTypeEnum, object>? ArgumentMap) : Commands(ArgumentMap)
{
    public override Result<Password> Execute()
    {
        throw new NotImplementedException();
    }
}