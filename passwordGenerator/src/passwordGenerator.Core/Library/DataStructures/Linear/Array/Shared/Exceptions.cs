using static passwordGenerator.Core.Library.DataStructures.Linear.Array.Shared.Constants;

namespace passwordGenerator.Core.Library.DataStructures.Linear.Array.Shared;

public static class Exceptions
{
    public static readonly Exception InvalidListCapacityException = new ApplicationException(ErrorInvalidCapacity);
    public static readonly Exception ListEmptyException = new ApplicationException(ErrorListEmpty);
    public static readonly Exception InvalidIndexException = new ApplicationException(ErrorInvalidIndex);
    public static readonly Exception ItemDoesNotExistException = new ApplicationException(ErrorItemDoesNotExist);
}