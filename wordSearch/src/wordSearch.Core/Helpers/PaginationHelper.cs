using static wordSearch.Core.Helpers.ConsoleHelper;
using static wordSearch.Core.Helpers.OutputHelper;
using static wordSearch.Core.Shared.Constants;
using static wordSearch.Core.Shared.Values;

namespace wordSearch.Core.Helpers;

public static class PaginationHelper
{
    public static void Paginate(List<string> suggestions, int pageIndex)
    {
        if (suggestions.Count == 0)
        {
            ReadKey(warningMessage);
            return;
        }

        HideCurosor();

        int totalPages = 1 + suggestions.Count / SizePerPage;
        List<string> paginated;
        bool shouldExit = false;
        while (!shouldExit)
        {
            paginated = Page(suggestions, pageIndex, SizePerPage);
            OutputToConsole(paginated, pageIndex);
            shouldExit = GetNextPage(totalPages, ref pageIndex);
        }

        ShowCursor();
    }

    public static bool GetNextPage(int totalPages, ref int pageIndex)
    {
        ConsoleKeyInfo consoleKeyInfo = ReadKey();
        if (consoleKeyInfo.Key == ConsoleKey.RightArrow)
        {
            pageIndex = (pageIndex + 1) % totalPages;
        }
        else if (consoleKeyInfo.Key == ConsoleKey.LeftArrow)
        {
            pageIndex = (totalPages + pageIndex - 1) % totalPages;
        }
        else
        {
            return true;
        }

        return false;
    }

    public static List<string> Page(
        List<string> source,
        int pageNo,
        int pageSize)
    {
        return [.. source.Skip(pageNo * pageSize).Take(pageSize)];
    }
}