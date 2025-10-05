using static passwordGenerator.Core.Shared.Regexes;
using static passwordGenerator.Core.Shared.Values;

namespace passwordGenerator.Core.Utility;

public static class ValidationUtility
{
    public static bool IsValidBooleanInput(string? value, out bool isTrue)
    {
        isTrue = true;

        if (string.IsNullOrEmpty(value))
        {
            return true;
        }

        return YesNoInputRegex().IsMatch(value)
        && allBooleanInputsMap.TryGetValue(value, out isTrue);
    }
}