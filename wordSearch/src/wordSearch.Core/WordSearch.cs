using wordSearch.Core.Abstractions;
using wordSearch.Core.Shared.State;
using static wordSearch.Core.Helpers.ApplicationHelper;
using static wordSearch.Core.Helpers.ArgumentHelper;
using static wordSearch.Core.Helpers.ControllerHelper;

namespace wordSearch.Core;

public static class WordSearch
{
    public static void Search(string[] args)
    {
        Result<Arguments> arguments = GetArguments(args);
        if (arguments.HasErrors)
        {
            HandleError(arguments.Errors);
        }

        Result<Controller> controller = GetController(arguments.Value);
        if (controller.HasErrors)
        {
            HandleError(controller.Errors);
        }

        Result<string> result = SetController(controller.Value);
        if (result.HasErrors)
        {
            HandleError(result.Errors);
        }
    }
}