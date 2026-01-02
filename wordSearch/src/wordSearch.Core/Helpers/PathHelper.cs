namespace wordSearch.Core.Helpers;

public static class PathHelper
{
    public static bool IsValidInputFilePath(object? pathObject, out string validated)
    {
        validated = string.Empty;

        if (pathObject is not string path || Directory.Exists(path) || !Path.Exists(path))
        {
            return false;
        }

        validated = path;

        return true;
    }

    public static bool IsValidOutputFilePath(object? pathObject, out string validated)
    {
        validated = string.Empty;

        if (pathObject is not string path)
        {
            return false;
        }

        string? directory = Path.GetDirectoryName(path);
        if (string.IsNullOrEmpty(directory))
        {
            return false;
        }

        validated = path;

        return true;
    }
}