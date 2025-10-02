using passwordGenerator.Core.Abstractions;
using passwordGenerator.Core.Controllers;
using passwordGenerator.Core.Enums;
using passwordGenerator.Core.Library.DataStructures.NonLinear.HashMap;
using passwordGenerator.Core.Shared;
using static passwordGenerator.Core.Enums.ArgumentTypeEnum;

namespace passwordGenerator.Core.Helpers;

public static class ControllerHelper
{
    public static Result<Commands> GetCommands(Arguments arguments)
    {
        HashMap<ArgumentTypeEnum, object> argumentMap = arguments.ArgumentMap;
        if (argumentMap.TryGetValue(Help, out _))
        {
            return new(new HelpController(argumentMap));
        }

        if (argumentMap.TryGetValue(Interactive, out _))
        {
            return new(new InteractiveController(argumentMap));
        }

        return new(new GeneratorController(argumentMap));
    }

    public static Result<Password> SetCommands(Commands commands)
        => commands.Execute();
}