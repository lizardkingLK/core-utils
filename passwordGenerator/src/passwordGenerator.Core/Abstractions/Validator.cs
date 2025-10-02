using passwordGenerator.Core.Library.DataStructures.NonLinear.HashMap;
using passwordGenerator.Core.Shared;

namespace passwordGenerator.Core.Abstractions;

public abstract record Validator<T, K>(
    HashMap<K, object> Values,
    T Product,
    Validator<T, K>? Next)
where K : notnull
{
    public abstract Result<T> Validate();
}