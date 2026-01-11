using wordSearch.Core.Abstractions;
using wordSearch.Core.Enums;
using wordSearch.Core.Library.NonLinear.HashMaps;
using wordSearch.Core.Shared.State;

namespace wordSearch.Core.Controllers;

public record HelpController(
    HashMap<ArgumentTypeEnum, object>? ArgumentMap) : Controller(ArgumentMap)
{
    public override Result<string> Execute()
    {
        Console.WriteLine("help is here...");

        return new(string.Empty);
    }
}