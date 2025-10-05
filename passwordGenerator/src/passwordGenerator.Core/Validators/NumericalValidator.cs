using passwordGenerator.Core.Abstractions;
using passwordGenerator.Core.Builder;
using passwordGenerator.Core.Enums;
using passwordGenerator.Core.Library.DataStructures.NonLinear.HashMap;
using passwordGenerator.Core.Shared;

namespace passwordGenerator.Core.Validators;

public record NumericalValidator(
    HashMap<ArgumentTypeEnum, object> Values,
    PasswordBuilder Product,
    Validator<PasswordBuilder, ArgumentTypeEnum>? Next)
: Validator<PasswordBuilder, ArgumentTypeEnum>(Values, Product, Next)
{
    public override Result<PasswordBuilder> Validate()
    {
        if (Values.Size == 0)
        {
            Product.UseNumeric();
            return Next?.Validate() ?? new(Product);
        }

        if (!Values.TryGetValue(ArgumentTypeEnum.Numeric, out _))
        {
            return Next?.Validate() ?? new(Product);
        }

        Product.UseNumeric();

        return Next?.Validate() ?? new(Product);
    }
}
