using passwordGenerator.Core.Abstractions;
using passwordGenerator.Core.Shared;
using static passwordGenerator.Core.Helpers.ArgumentHelper;
using static passwordGenerator.Core.Helpers.ControllerHelper;

namespace passwordGenerator.Core;

public class Generator
{
    public static Password Generate(string[] args)
    {
        Result<Arguments> arguments = GetArguments(args);
        if (arguments.Errors != null)
        {
            return new(Errors: arguments.Errors);
        }

        Result<Commands> commands = GetCommands(arguments.Data!);
        if (commands.Errors != null)
        {
            return new(Errors: commands.Errors);
        }

        Result<Password> result = SetCommands(commands.Data!);
        if (result.Errors != null)
        {
            return new(Errors: result.Errors);
        }

        return result.Data!;
    }
}
