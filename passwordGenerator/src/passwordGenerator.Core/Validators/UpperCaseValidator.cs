using passwordGenerator.Core.Abstractions;
using passwordGenerator.Core.Builder;
using passwordGenerator.Core.Enums;
using passwordGenerator.Core.Library.DataStructures.NonLinear.HashMap;
using passwordGenerator.Core.Shared;

namespace passwordGenerator.Core.Validators;

public record UpperCaseValidator(
    HashMap<ArgumentTypeEnum, object> Values,
    PasswordBuilder Product,
    Validator<PasswordBuilder, ArgumentTypeEnum>? Next)
: Validator<PasswordBuilder, ArgumentTypeEnum>(Values, Product, Next)
{
    public override Result<PasswordBuilder> Validate()
    {
        if (!Values.TryGetValue(ArgumentTypeEnum.UpperCase, out _))
        {
            return Next?.Validate() ?? new(Product);
        }

        Product.UseUpperCase();

        return Next?.Validate() ?? new(Product);
    }
}
