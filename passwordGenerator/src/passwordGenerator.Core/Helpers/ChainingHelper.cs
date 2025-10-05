using passwordGenerator.Core.Abstractions;
using passwordGenerator.Core.Builder;
using passwordGenerator.Core.Enums;
using passwordGenerator.Core.Library.DataStructures.NonLinear.HashMap;
using passwordGenerator.Core.Shared;
using passwordGenerator.Core.Validators;

namespace passwordGenerator.Core.Helpers;

public static class ChainingHelper
{
    public static
    Result<Validator<PasswordBuilder, ArgumentTypeEnum>> GetArgumentValidator(
        HashMap<ArgumentTypeEnum, object> argumentMap)
    {
        try
        {
            PasswordBuilder passwordBuilder = new();

            SymbolicalValidator symbolicValidator = new(argumentMap, passwordBuilder, null);
            UpperCaseValidator upperCaseValidator = new(argumentMap, passwordBuilder, symbolicValidator);
            LowerCaseValidator lowerCaseValidator = new(argumentMap, passwordBuilder, upperCaseValidator);
            NumericalValidator numericalValidator = new(argumentMap, passwordBuilder, lowerCaseValidator);
            CountValidator countValidator = new(argumentMap, passwordBuilder, numericalValidator);

            return new(countValidator);
        }
        catch (Exception ex)
        {
            return new(null, ex.Message);
        }
    }

    public static Result<PasswordBuilder> SetArguments(
        Validator<PasswordBuilder, ArgumentTypeEnum> validator)
    => validator.Validate();
}