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
}