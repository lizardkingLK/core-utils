using passwordGenerator.Core.Abstractions;
using passwordGenerator.Core.Builder;
using passwordGenerator.Core.Views;
using static passwordGenerator.Core.Utility.ConsoleUtility;
using static passwordGenerator.Core.Utility.ValidationUtility;

namespace passwordGenerator.Core.Interactions;

public record LowerCaseInteraction(PasswordBuilder PasswordBuilder) : Interaction
{
    private readonly PasswordBuilder _passwordBuilder = PasswordBuilder;
    private readonly LowerCaseView _view = new();
    private bool isTrue = true;

    public override void Display() => WriteInformation(_view.Message!);

    public override void Prompt()
    {
        string? input;

        while (true)
        {
            WritePrompt();
            input = ReadInput();
            if (IsValidBooleanInput(input, out isTrue))
            {
                break;
            }

            WriteError(_view.Error!);
            Display();
        }
    }

    public override void Process()
    {
        if (isTrue)
        {
            _passwordBuilder.UseLowerCase();
        }
    }
}