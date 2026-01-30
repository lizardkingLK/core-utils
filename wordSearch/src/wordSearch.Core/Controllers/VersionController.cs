using System.Reflection;
using System.Runtime.InteropServices;
using wordSearch.Core.Abstractions;
using wordSearch.Core.Enums;
using wordSearch.Core.Library.NonLinear.HashMaps;
using wordSearch.Core.Shared.State;

namespace wordSearch.Core.Controllers;

public record VersionController(HashMap<ArgumentTypeEnum, object>? ArgumentMap) : Controller(ArgumentMap)
{
    public override Result<string> Execute()
    {
        Console.WriteLine(BuildVersionScreen());

        return new Result<string>(string.Empty);
    }

    private static string BuildVersionScreen()
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        string version = assembly
        .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
        .InformationalVersion
        ?? assembly.GetName().Version?.ToString() ?? "1.0.0";

        return $@"
{nameof(WordSearch)} v{version}
Build:      {DateTime.UtcNow}
Runtime:    {RuntimeInformation.FrameworkDescription}
OS/Arch:    {RuntimeInformation.OSDescription} / {RuntimeInformation.ProcessArchitecture}
{"\nGithub @lizardkinglk"}";
    }
}