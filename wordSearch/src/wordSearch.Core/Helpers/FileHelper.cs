using System.Reflection;
using static wordSearch.Core.Helpers.ApplicationHelper;

namespace wordSearch.Core.Helpers;

public static class FileHelper
{
    public static IEnumerable<string> ReadAllLines(string filePath)
    {
        foreach (string line in File.ReadAllLines(filePath))
        {
            yield return line;
        }
    }

    public static IEnumerable<string> ReadAllAssetLines(string resource)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();

        using Stream? resourceStream = assembly.GetManifestResourceStream(resource);
        if (resourceStream == null)
        {
            HandleError($"error. cannot find the resource \"{resource}\"");
        }

        using StreamReader resourceStreamReader = new(resourceStream!);

        while (resourceStreamReader.Peek() != -1)
        {
            yield return resourceStreamReader.ReadLine()!;
        }
    }
}