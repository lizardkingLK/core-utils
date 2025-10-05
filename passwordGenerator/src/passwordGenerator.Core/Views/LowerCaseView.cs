using passwordGenerator.Core.Abstractions;

namespace passwordGenerator.Core.Views;

public record LowerCaseView : View
{
    public override string Message => """
    
    Select if lowercase letters should include in the password? (Y/n)
    """;

    public override string Data => string.Empty;

    public override string Error => """
    error. Invalid input was given.
    """;
}