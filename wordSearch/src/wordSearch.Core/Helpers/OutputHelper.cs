using System.Text;
using wordSearch.Core.Enums;
using wordSearch.Core.Library.NonLinear.HashMaps;
using wordSearch.Core.Shared.State;
using static wordSearch.Core.Helpers.ApplicationHelper;
using static wordSearch.Core.Helpers.ConsoleHelper;
using static wordSearch.Core.Helpers.PathHelper;
using static wordSearch.Core.Shared.Constants;
using static wordSearch.Core.Shared.Values;

namespace wordSearch.Core.Helpers;

public static class OutputHelper
{
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

    public static void OutputToConsole(List<string> paginated, int pageIndex)
    {
        responseMessage.Content = string.Join(Environment.NewLine, paginated);
        ClearLines(responseMessage.Y);
        WriteLine(responseMessage);
        WriteLine(controlMessage);

        pageMessage.Content = new string(SymbolSpace, pageMessage.Size);
        WriteLine(pageMessage);

        pageMessage.Content = string.Format(PageFormat, pageIndex + 1);
        pageMessage.X = SpacingInfoPage + ((string)controlMessage.Content!).Length;
        WriteLine(pageMessage);
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
                    WriteNextLine(infoMessage);
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

        outputBuilder = suggestions.Aggregate(
            outputBuilder,
            (outputBuilder, line) => outputBuilder.AppendLine(line));

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

    public static void OutputSuggestions(
        HashMap<ArgumentTypeEnum, object> Arguments,
        IEnumerable<string> suggestions)
    {
        _ = Arguments.TryGetValue(ArgumentTypeEnum.Count, out object? countObject);
        if (Arguments.TryGetValue(ArgumentTypeEnum.OutputPath, out object? outputPathObject))
        {
            OutputToFile(suggestions, countObject, outputPathObject);
        }
        else
        {
            OutputToConsole(suggestions, countObject, Console.IsOutputRedirected);
        }
    }

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