using wordSearch.Core.Enums;
using wordSearch.Core.Library.NonLinear.HashMaps;
using wordSearch.Core.Shared.State;
using wordSearch.Core.Validators;
using static wordSearch.Core.Shared.Exceptions;
using static wordSearch.Core.Shared.Regexes;
using static wordSearch.Core.Shared.Values;

namespace wordSearch.Core.Helpers;

public static class ArgumentHelper
{
    public static Result<Arguments> GetArguments(string[] arguments)
    {
        Result<HashMap<ArgumentTypeEnum, object>> collectResult = SetArguments(arguments);
        if (collectResult.HasErrors)
        {
            return new(null, collectResult.Errors);
        }

        Result<Arguments> validateResult = ValidateArguments(collectResult.Value);
        if (validateResult.HasErrors)
        {
            return new(null, validateResult.Errors);
        }

        return validateResult;
    }

    private static Result<Arguments> ValidateArguments(HashMap<ArgumentTypeEnum, object> argumentsMap)
    {
        OutputPathValidator outputPath = new(argumentsMap);
        InputPathValidator inputPath = new(argumentsMap, outputPath);
        CountValidator count = new(argumentsMap, inputPath); // TODO: int type range min max
        QueryValidator query = new(argumentsMap, count); // TODO: not longer than 30 chars
        InteractiveValidator interactive = new(argumentsMap, query);
        VersionValidator version = new(argumentsMap, interactive);
        HelpValidator help = new(argumentsMap, version);

        Result<Arguments> validatedResult = help.Validate();
        if (validatedResult.HasErrors)
        {
            return validatedResult;
        }

        return new(new(argumentsMap));
    }

    private static Result<HashMap<ArgumentTypeEnum, object>> SetArguments(string[] arguments)
    {
        HashMap<ArgumentTypeEnum, object> argumentMap = [];

        int length = arguments.Length;
        string argument;
        string? nextArgument;
        string[] argumentArray;
        Result<bool>? validationResult;
        for (int i = 0; i < length; i++)
        {
            argument = arguments[i];
            nextArgument = arguments.ElementAtOrDefault(i + 1);
            if (FullArgumentRegex().IsMatch(argument))
            {
                argumentArray = [argument.ToLower()[2..argument.Length]];
                validationResult = ResolveArguments(
                    argumentMap,
                    nextArgument,
                    ref i,
                    argumentArray);
            }
            else if (PrefixedArgumentRegex().IsMatch(argument))
            {
                argumentArray = [.. argument.ToLower()[1..argument.Length].Select(item => item.ToString())];
                validationResult = ResolveArguments(argumentMap, nextArgument, ref i, argumentArray);
            }
            else
            {
                return new(null, InvalidArgumentException(i, argument));
            }

            if (validationResult.HasErrors)
            {
                return new(null, validationResult.Errors);
            }
        }

        return new(argumentMap);
    }

    private static Result<bool> ResolveArguments(
        HashMap<ArgumentTypeEnum, object> argumentMap,
        string? nextArgument,
        ref int index,
        string[] arguments)
    {
        if (arguments.Length == 0)
        {
            return new(false, NoArgumentsException(index));
        }

        foreach (string argument in arguments)
        {
            if (!allArgumentsMap.TryGetValue(
                argument,
                out (ArgumentTypeEnum, object, bool) keyValueRequired))
            {
                return new(false, InvalidArgumentException(index, argument));
            }

            (ArgumentTypeEnum type, object value, bool isRequired) = keyValueRequired;
            if (!argumentMap.TryAdd(type, value))
            {
                return new(false, DuplicateArgumentException(type.ToString()));
            }

            if (!isRequired)
            {
                continue;
            }

            if (string.IsNullOrEmpty(nextArgument))
            {
                return new(false, RequiredArgumentException(type.ToString()));
            }

            argumentMap[type] = nextArgument;
            index++;
        }

        return new(true);
    }
}