using wordSearch.Core.Enums;
using wordSearch.Core.Library.NonLinear.HashMaps;

namespace wordSearch.Core.Shared.State;

public record Arguments(HashMap<ArgumentTypeEnum, object> ArgumentMap);