using passwordGenerator.Core.Abstractions;

namespace passwordGenerator.Core.Views;

public record UpperCaseView : View
{
    public override string Message => """
    
    Select if uppercase letters should include in the password? (Y/n)
    """;

    public override string Data => string.Empty;

    public override string Error => """
    error. Invalid input was given.
    """;
}