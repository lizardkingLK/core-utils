using passwordGenerator.Core.Abstractions;
using passwordGenerator.Core.Builder;
using passwordGenerator.Core.Enums;
using passwordGenerator.Core.Library.DataStructures.NonLinear.HashMap;
using passwordGenerator.Core.Shared;
using static passwordGenerator.Core.Shared.Errors;
using static passwordGenerator.Core.Shared.Constants;

namespace passwordGenerator.Core.Validators;

public record CountValidator(
    HashMap<ArgumentTypeEnum, object> Values,
    PasswordBuilder Product,
    Validator<PasswordBuilder, ArgumentTypeEnum>? Next)
: Validator<PasswordBuilder, ArgumentTypeEnum>(Values, Product, Next)
{
    public override Result<PasswordBuilder> Validate()
    {
        if (Values.Size == 0)
        {
            Product.UseCount(MinCount);
            return Next?.Validate() ?? new(Product);
        }

        bool hasCountValue = Values.TryGetValue(ArgumentTypeEnum.Count, out object? value);
        if (!hasCountValue)
        {
            return Next?.Validate() ?? new(Product);
        }

        if (!int.TryParse((value ?? string.Empty).ToString(), out int _count))
        {
            return new(null, InvalidArgumentException(
                ArgumentTypeEnum.Count.ToString(),
                value!.ToString()!));
        }

        if (_count is < MinCount or > MaxCount)
        {
            return new(null, InvalidArgumentException(
                ArgumentTypeEnum.Count.ToString(),
                value!.ToString()!, MinCount, MaxCount));
        }

        if (Values.Size == 1 && hasCountValue)
        {
            return new(Product.UseAll(_count));
        }

        Product.UseCount(_count);

        return Next?.Validate() ?? new(Product);
    }
}
