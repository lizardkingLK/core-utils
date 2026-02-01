namespace wordSearch.Core.Shared;

public static class Constants
{
    public const char SymbolSpace = ' ';
    public const char SymbolPlace = '_';
    public const int SizePerPage = 10;
    public const int SpacingInfoPage = 10;
    public const int MinResultCount = 1000;
    public const int MaxQueryLength = 30;
    public const int HintDurationSeconds = 3;
    public const string DictionaryPath = "./Assets/Dictionary/index.txt";
    public const string PageFormat = "Page {0}";
    public const string QueryMessage = "Enter keyword: > ";
    public const string DictionarySentinel = ".";
    public const string AppUrl = "https://github.com/lizardkingLK/core-utils/tree/main/wordSearch";
}