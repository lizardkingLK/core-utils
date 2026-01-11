namespace wordSearch.Core.Enums;

public enum ArgumentTypeEnum : byte
{
    Help,
    Version,
    Interactive,
    Query,  // TODO: regex style with underscores and stars only
    Count,
    InputPath,
    OutputPath,
}