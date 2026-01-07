using System.Text;
using wordSearch.Core.Shared.State;
using static wordSearch.Core.Helpers.ApplicationHelper;
using static wordSearch.Core.Helpers.ConsoleHelper;
using static wordSearch.Core.Helpers.PathHelper;
using static wordSearch.Core.Shared.Constants;

namespace wordSearch.Core.Helpers;

public static class OutputHelper
{
    private readonly static Message _infoMessage = new()
    {
        Content = "info. only 1000 suggestions were written. use --output flag to get all items",
        ForegroundColor = ConsoleColor.DarkYellow,
        Y = Console.WindowHeight - 2,
    };

    public static void OutputToConsole(
        IEnumerable<string> suggestions,
        object? countObject,
        bool isOutputRedirected)
    {
        if (isOutputRedirected)
        {
            OutputToConsole(suggestions, GetCount(countObject));
        }
        else
        {
            OutputToConsole(suggestions, GetMin(GetCount(countObject)));
        }
    }

    private static void OutputToConsole(IEnumerable<string> suggestions, int? maxCount = null)
    {
        StringBuilder outputBuilder = new();

        if (maxCount != null)
        {
            int count = 0;
            Message outputMessage = new();
            foreach (string suggestion in suggestions)
            {
                if (count == MinResultCount)
                {
                    WriteNextLine(_infoMessage);
                    SetCursor(Console.WindowHeight - 1, 0);
                }

                if (count == maxCount)
                {
                    break;
                }

                outputMessage.Content = suggestion;
                WriteNextLine(outputMessage);
                count++;
            }

            return;
        }

        outputBuilder = suggestions.Aggregate(outputBuilder, AppendLine);
        WriteLine(new Message
        {
            Content = outputBuilder.ToString(),
            Y = 1,
        });
    }

    public static void OutputToFile(IEnumerable<string> suggestions, object? countObject, object? outputPathObject)
    {
        if (!IsValidOutputFilePath(outputPathObject, out string outputPath))
        {
            HandleError("error. cannot find directory. invalid output path given");
        }

        using StreamWriter fileWriter = new(outputPath);

        int? maxCount = GetCount(countObject);
        if (maxCount.HasValue)
        {
            foreach (string suggestion in suggestions.Take(maxCount.Value))
            {
                fileWriter.WriteLine(suggestion);
            }

            return;
        }

        foreach (string suggestion in suggestions)
        {
            fileWriter.WriteLine(suggestion);
        }
    }

    private static StringBuilder AppendLine(
        StringBuilder outputBuilder,
        string line) => outputBuilder.AppendLine(line);

    private static int? GetMin(int? value)
    {
        if (value == null || value >= MinResultCount)
        {
            return MinResultCount;
        }

        return value.Value;
    }

    private static int? GetCount(object? countObject)
    {
        if (countObject == null)
        {
            return null;
        }

        if (countObject is not string countString
        || !int.TryParse(countString, out int count)
        || count <= 0 || count >= int.MaxValue)
        {
            HandleError("error. invalid count argument was given");
            return null;
        }

        return count;
    }
}