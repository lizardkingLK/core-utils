using passwordGenerator.Core.Abstractions;
using passwordGenerator.Core.Builder;
using passwordGenerator.Core.Enums;
using passwordGenerator.Core.Library.DataStructures.NonLinear.HashMap;
using passwordGenerator.Core.Shared;
using static passwordGenerator.Core.Helpers.InteractionHelper;
using static passwordGenerator.Core.Utility.ConsoleUtility;

namespace passwordGenerator.Core.Controllers;

public record InteractiveController(
    HashMap<ArgumentTypeEnum, object>? ArgumentMap)
: Commands(ArgumentMap)
{
    public override Result<Password> Execute()
    {
        IEnumerable<Interaction> interactions =
        GetInteractions(out PasswordBuilder passwordBuilder);
        foreach (Interaction? interaction in interactions)
        {
            if (interaction == null)
            {
                continue;
            }

            interaction.Display();
            interaction.Prompt();
            interaction.Process();
        }

        WriteData("\nYour password is below");

        return new(passwordBuilder.Build());
    }
}