namespace wordSearch.Core.Shared;

public static class Exceptions
{
    public static string NoArgumentsException(int index)
    => $@"error. argument given at index ""{index}"" are empty";
    public static string InvalidArgumentException(int index, string value)
    => $@"error. argument given at index ""{index}"" with value ""{value}"" is invalid";
    public static string RequiredArgumentException(string value)
    => $@"error. required arguments for type ""{value}"" were not given";
    public static string DuplicateArgumentException(string value)
    => $@"error. duplicate arguments were given for type ""{value}""";
}