using passwordGenerator.Core.Enums;
using passwordGenerator.Core.Library.DataStructures.NonLinear.HashMap;
using passwordGenerator.Core.Shared;
using static passwordGenerator.Core.Shared.Errors;
using static passwordGenerator.Core.Shared.Regexes;
using static passwordGenerator.Core.Shared.Values;

namespace passwordGenerator.Core.Helpers;

public static class ArgumentHelper
{
    public static Result<Arguments> GetArguments(string[] arguments)
    {
        HashMap<ArgumentTypeEnum, object> argumentMap = new();
        
        int length = arguments.Length;
        string argument;
        string? nextArgument;
        string[] argumentArray;
        Result<bool>? validationResult = null;
        for (int i = 0; i < length; i++)
        {
            argument = arguments[i];
            nextArgument = arguments.ElementAtOrDefault(i + 1)?.ToLower();
            if (FullArgumentRegex().IsMatch(argument))
            {
                argumentArray = [argument.ToLower()[2..argument.Length]];
                validationResult = ResolveArguments(argumentMap, nextArgument, ref i, argumentArray);
            }
            else if (PrefixedArgumentRegex().IsMatch(argument))
            {
                argumentArray = [.. argument.ToLower()[1..argument.Length].ToCharArray().Select(item => item.ToString())];
                validationResult = ResolveArguments(argumentMap, nextArgument, ref i, argumentArray);
            }
            else
            {
                return new(null, InvalidArgumentException(i, argument));
            }

            if (!validationResult.Data)
            {
                return new(null, validationResult.Errors);
            }
        }

        return new(new(argumentMap));
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
            if (!allArgumentsMap.TryGetValue(argument, out (ArgumentTypeEnum, object, bool) keyValueRequired))
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