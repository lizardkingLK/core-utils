using wordSearch.Core.Abstractions;
using wordSearch.Core.Enums;
using wordSearch.Core.Library.NonLinear.HashMaps;
using wordSearch.Core.Shared.State;

namespace wordSearch.Core.Controllers;

public record VersionController(HashMap<ArgumentTypeEnum, object>? ArgumentMap) : Controller(ArgumentMap)
{
    public override Result<string> Execute()
    {
        Console.WriteLine("version is here...");

        return new Result<string>("");
    }
}