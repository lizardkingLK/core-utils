using passwordGenerator.Core.Abstractions;
using passwordGenerator.Core.Builder;
using static passwordGenerator.Core.Utility.ConsoleUtility;

namespace passwordGenerator.Core.Interactions;

public record HelpInteraction(string View) : Interaction<PasswordBuilder>
{
    public override string View { get; init; } = View;

    public override void Display() => WriteInformation(View);

    public override void Prompt(PasswordBuilder builder)
    {
           
    }
}