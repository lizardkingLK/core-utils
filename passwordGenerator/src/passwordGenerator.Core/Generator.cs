using passwordGenerator.Core.Abstractions;
using passwordGenerator.Core.Shared;
using static passwordGenerator.Core.Helpers.ArgumentHelper;
using static passwordGenerator.Core.Helpers.ControllerHelper;
using static passwordGenerator.Core.Helpers.ResultHelper;

namespace passwordGenerator.Core;

public class Generator
{
    public static void Generate(string[] args)
    {
        Result<Arguments> arguments = GetArguments(args);
        if (arguments.Errors != null)
        {
            HandleError(arguments.Errors);
        }

        Result<Commands> commands = GetCommands(arguments.Data!);
        if (commands.Errors != null)
        {
            HandleError(commands.Errors);
        }

        Result<Password> result = SetCommands(commands.Data!);
        if (result.Errors != null)
        {
            HandleError(result.Errors);
        }

        result.Data!.Execute();
    }
}
