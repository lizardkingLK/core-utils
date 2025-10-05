using passwordGenerator.Core.Abstractions;
using passwordGenerator.Core.Builder;
using passwordGenerator.Core.Views;
using static passwordGenerator.Core.Utility.ConsoleUtility;
using static passwordGenerator.Core.Shared.Constants;

namespace passwordGenerator.Core.Interactions;

public record CountInteraction(PasswordBuilder PasswordBuilder) : Interaction
{
    private readonly PasswordBuilder _passwordBuilder = PasswordBuilder;
    private readonly CountView _view = new();
    private int output = MinCount;

    public override void Display() => WriteInformation(_view.Message!);

    public override void Prompt()
    {
        string? input;

        while (true)
        {
            WritePrompt();
            input = ReadInput();
            if (IsValidNumericInput(input, out output))
            {
                break;
            }

            WriteError(_view.Error!);
            Display();
        }
    }

    public override void Process() => _passwordBuilder.UseCount(output);

    private static bool IsValidNumericInput(string? value, out int output)
    {
        output = MinCount;

        if (string.IsNullOrEmpty(value))
        {
            return true;
        }

        return int.TryParse(value, out output)
        && output is >= MinCount and <= MaxCount;
    }
}