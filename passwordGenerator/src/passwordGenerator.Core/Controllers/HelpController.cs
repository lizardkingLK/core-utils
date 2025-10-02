using passwordGenerator.Core.Abstractions;
using passwordGenerator.Core.Enums;
using passwordGenerator.Core.Library.DataStructures.NonLinear.HashMap;
using passwordGenerator.Core.Shared;
using passwordGenerator.Core.Views.Help;

namespace passwordGenerator.Core.Controllers;

public record HelpController(HashMap<ArgumentTypeEnum, object>? ArgumentMap) : Commands(ArgumentMap)
{
    public override Result<Password> Execute()
    {
        return new Result<Password>(new Password(null, HelpView.Data, null));
    }
}