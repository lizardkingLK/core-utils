using passwordGenerator.Core.Abstractions;
using passwordGenerator.Core.Enums;
using passwordGenerator.Core.Library.DataStructures.NonLinear.HashMap;
using passwordGenerator.Core.Shared;
using passwordGenerator.Core.Views;

namespace passwordGenerator.Core.Controllers;

public record HelpController(HashMap<ArgumentTypeEnum, object>? ArgumentMap) : Commands(ArgumentMap)
{
    private readonly HelpView _helpView = new();

    public override Result<Password> Execute()
    {
        return new(new(Information: _helpView.Data));
    }
}