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
        else if (argumentMap.TryGetValue(ArgumentTypeEnum.Version, out _))
        {
            return new(new VersionController(argumentMap));
        }
        else if (argumentMap.TryGetValue(Dictionary, out _))
        {
            return new(new DictionaryController(argumentMap));
        }
        else if (argumentMap.TryGetValue(Anagrams, out _))
        {
            return new(new AnagramsController(argumentMap));
        }
        else if (argumentMap.TryGetValue(InputPath, out _))
        {
            return new(new SuggestionController(argumentMap));
        }
        else if (Console.IsInputRedirected)
        {
            return new (new RedirectionController(argumentMap));
        }

        return new(null, "error. invalid controller mapping");
    }

    public static Result<string> SetController(Controller controller)
    {
        return controller.Execute();
    }
}