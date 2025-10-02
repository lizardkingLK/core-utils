using passwordGenerator.Core.Abstractions;
using passwordGenerator.Core.Builder;
using passwordGenerator.Core.Enums;
using passwordGenerator.Core.Library.DataStructures.NonLinear.HashMap;
using passwordGenerator.Core.Shared;
using static passwordGenerator.Core.Helpers.InteractionHelper;

namespace passwordGenerator.Core.Controllers;

public record InteractiveController(
    HashMap<ArgumentTypeEnum, object>? ArgumentMap)
: Commands(ArgumentMap)
{
    public override Result<Password> Execute()
    {
        PasswordBuilder passwordBuilder;
        
        foreach (Interaction<PasswordBuilder> interaction in GetInteractions(out passwordBuilder))
        {
            interaction.Display();
            interaction.Prompt(passwordBuilder);
        }

        return new(passwordBuilder.Build());
    }
}