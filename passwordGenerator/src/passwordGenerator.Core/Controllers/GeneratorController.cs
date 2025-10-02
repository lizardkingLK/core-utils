using passwordGenerator.Core.Abstractions;
using passwordGenerator.Core.Builder;
using passwordGenerator.Core.Enums;
using passwordGenerator.Core.Library.DataStructures.NonLinear.HashMap;
using passwordGenerator.Core.Shared;
using static passwordGenerator.Core.Helpers.ChainingHelper;

namespace passwordGenerator.Core.Controllers;

public record GeneratorController(HashMap<ArgumentTypeEnum, object>? ArgumentMap) : Commands(ArgumentMap)
{
    public override Result<Password> Execute()
    {
        Result<Validator<PasswordBuilder, ArgumentTypeEnum>> validator = GetArgumentValidator(ArgumentMap!);
        if (validator.Errors != null)
        {
            return new(null, validator.Errors);
        }

        Result<PasswordBuilder> builder = SetArguments(validator.Data!);
        if (builder.Errors != null)
        {
            return new(null, builder.Errors);
        }

        return new(builder.Data!.Build());
    }
}