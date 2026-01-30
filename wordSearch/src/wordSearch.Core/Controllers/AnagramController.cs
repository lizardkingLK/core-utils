using wordSearch.Core.Abstractions;
using wordSearch.Core.Library.NonLinear.HashMaps;
using wordSearch.Core.Shared.State;

namespace wordSearch.Core.Controllers;

public record AnagramController(
    HashMap<Enums.ArgumentTypeEnum, object>? ArgumentMap) : Controller(ArgumentMap)
{
    public override Result<string> Execute()
    {
        Console.WriteLine("anagrams are here");

        return new(string.Empty);
    }
}