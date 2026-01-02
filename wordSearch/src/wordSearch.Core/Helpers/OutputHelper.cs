using System.Text;
using wordSearch.Core.Shared.State;
using static wordSearch.Core.Helpers.ApplicationHelper;
using static wordSearch.Core.Helpers.ConsoleHelper;
using static wordSearch.Core.Helpers.PathHelper;

namespace wordSearch.Core.Helpers;

public static class OutputHelper
{
    public static void OutputToConsole(IEnumerable<string> suggestions, bool isOutputRedirected)
    {
        if (isOutputRedirected)
        {
            OutputToConsole(suggestions);
        }
        else
        {
            OutputToConsole(suggestions, 1000);
        }
    }

    private static void OutputToConsole(IEnumerable<string> suggestions, int? maxCount = null)
    {
        StringBuilder outputBuilder = new();

        if (maxCount != null)
        {
            WriteLine(new Message
            {
                
            });

            outputBuilder = suggestions.Take(maxCount.Value).Aggregate(outputBuilder, AppendLine);
            
            WriteLine(new Message
            {
                Content = outputBuilder.ToString(),
                Y = 1,
            });
            return;
        }

        outputBuilder = suggestions.Aggregate(outputBuilder, AppendLine);
        WriteLine(new Message
        {
            Content = outputBuilder.ToString(),
            Y = 1,
        });
    }

    public static void OutputToFile(IEnumerable<string> suggestions, object? outputPathObject)
    {
        if (!IsValidOutputFilePath(outputPathObject, out string outputPath))
        {
            HandleError("error. cannot find directory. invalid output path given");
        }

        using StreamWriter fileWriter = new(outputPath);

        foreach (string suggestion in suggestions)
        {
            fileWriter.WriteLine(suggestion);
        }
    }

    private static StringBuilder AppendLine(
        StringBuilder outputBuilder,
        string line) => outputBuilder.AppendLine(line);
}