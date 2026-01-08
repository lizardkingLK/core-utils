using static wordSearch.Core.Helpers.ConsoleHelper;
using static wordSearch.Core.Helpers.OutputHelper;
using static wordSearch.Core.Shared.Constants;
using static wordSearch.Core.Shared.Values;

namespace wordSearch.Core.Helpers;

public static class PaginationHelper
{
    public static void Paginate(List<string> suggestions, ConsoleKey key, ref int pageIndex)
    {
        if (suggestions.Count == 0)
        {
            ClearLines(responseMessage.Y);
            return;
        }

        HideCursor();

        int totalPages = 1 + suggestions.Count / SizePerPage;
        GetNextPage(totalPages, ref pageIndex, key);

        List<string> paginated = Page(suggestions, pageIndex, SizePerPage);
        OutputToConsole(paginated, pageIndex);

        ShowCursor();
    }

    public static void GetNextPage(int totalPages, ref int pageIndex, ConsoleKey key)
    {
        if (key == ConsoleKey.RightArrow)
        {
            pageIndex = (pageIndex + 1) % totalPages;
        }
        else if (key == ConsoleKey.LeftArrow)
        {
            pageIndex = (totalPages + pageIndex - 1) % totalPages;
        }
    }

    public static List<string> Page(
        List<string> source,
        int pageNo,
        int pageSize)
    {
        return [.. source.Skip(pageNo * pageSize).Take(pageSize)];
    }
}