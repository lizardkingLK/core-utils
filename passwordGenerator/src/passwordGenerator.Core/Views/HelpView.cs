using passwordGenerator.Core.Abstractions;

namespace passwordGenerator.Core.Views;

public record HelpView : View
{
    public override string Message => """

    Would you like to see Help? (Y/n)
    """;

    public override string Data => """

    --------------------------------
    |      PASSWORD GENERATOR      |
    --------------------------------

    USAGE       = pass [OPTIONS]

    OPTIONS

    help        = [-[h|-help]]
    * To display this view

    interactive = [-[i|-interactive]]
    * To interactively build a password

    numeric     = [-[n|-numeric]]
    * To include numerical values (0-9) in password

    lowercase   = [-[l|-lowercase]]
    * To include lowercase letters in password

    uppercase   = [-[u|-uppercase]]
    * To include uppercase letters in password

    symbolic    = [-[s|-symbolic]]
    * To include symbol inputs in password

    count       = [-[c|-count]] (16-128)
    * To include the length of password.
      Between 16 to 128 chars. inclusive.

    """;

    public override string Error => """
    error. Invalid input was given.
    """;
}