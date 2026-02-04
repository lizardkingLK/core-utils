using wordSearch.Core.Abstractions;
using wordSearch.Core.Enums;
using wordSearch.Core.Library.NonLinear.HashMaps;
using wordSearch.Core.Shared.State;
using static wordSearch.Core.Shared.Constants;

namespace wordSearch.Core.Controllers;

public record VersionController(
    HashMap<ArgumentTypeEnum, object> Arguments) : Controller(Arguments)
{
    public override Result<string> Execute()
    {
        Console.WriteLine("{0}\n{1}\n{2}",
        nameof(WordSearch),
        Environment.ProcessPath,
        AppUrl);

        return new Result<string>(string.Empty);
    }
}