using passwordGenerator.Core.Enums;
using passwordGenerator.Core.Library.DataStructures.NonLinear.HashMap;

namespace passwordGenerator.Core.Shared;

public record Arguments(HashMap<ArgumentTypeEnum, object> ArgumentMap);