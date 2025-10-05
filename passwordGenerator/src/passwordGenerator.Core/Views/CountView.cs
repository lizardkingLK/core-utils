using passwordGenerator.Core.Abstractions;

namespace passwordGenerator.Core.Views;

public record CountView : View
{
    public override string Message => """
    
    Please enter a number as the length of the password? (16-128 chars.)
    """;

    public override string Data => string.Empty;

    public override string Error => """
    error. Invalid password length input was given.
    """;
}