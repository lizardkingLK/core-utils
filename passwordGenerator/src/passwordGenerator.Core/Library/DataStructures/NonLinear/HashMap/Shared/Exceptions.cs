using static passwordGenerator.Core.Library.DataStructures.NonLinear.HashMap.Shared.Constants;

namespace passwordGenerator.Core.Library.DataStructures.NonLinear.HashMap.Shared;

public static class Exceptions
{
        public static readonly Exception KeyContainsException = new ApplicationException(ErrorKeyAlreadyContains);
        public static readonly Exception KeyDoesNotContainException = new ApplicationException(ErrorKeyDoesNotContain);
}