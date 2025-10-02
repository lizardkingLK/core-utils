using passwordGenerator.Core.Enums;
using passwordGenerator.Core.Library.DataStructures.NonLinear.HashMap;
using passwordGenerator.Core.Shared;

namespace passwordGenerator.Core.Abstractions;

public abstract record Commands(HashMap<ArgumentTypeEnum, object>? ArgumentMap)
{
    public abstract Result<Password> Execute();
}