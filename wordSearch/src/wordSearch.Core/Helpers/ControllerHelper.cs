using wordSearch.Core.Abstractions;
using wordSearch.Core.Controllers;
using wordSearch.Core.Enums;
using wordSearch.Core.Library.NonLinear.HashMaps;
using wordSearch.Core.Shared.State;
using static wordSearch.Core.Enums.ArgumentTypeEnum;

namespace wordSearch.Core.Helpers;

public static class ControllerHelper
{
    public static Result<Controller> GetController(Arguments arguments)
    {
        HashMap<ArgumentTypeEnum, object> argumentMap = arguments.ArgumentMap;

        if (argumentMap.TryGetValue(Help, out _))
        {
            return new(new HelpController(argumentMap));
        }

        return new(new SuggestionController(argumentMap));
    }

    public static Result<string> SetController(Controller controller)
    => controller.Execute();
}