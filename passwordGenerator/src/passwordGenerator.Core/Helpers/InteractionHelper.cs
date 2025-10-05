using passwordGenerator.Core.Abstractions;
using passwordGenerator.Core.Builder;
using passwordGenerator.Core.Interactions;
using passwordGenerator.Core.Library.DataStructures.Linear.Array;

namespace passwordGenerator.Core.Helpers;

public static class InteractionHelper
{
    public static IEnumerable<Interaction> GetInteractions(out PasswordBuilder passwordBuilder)
    {
        passwordBuilder = new();

        DynamicArray<Interaction> interactions = new(
            new HelpInteraction(passwordBuilder),
            new NumericInteraction(passwordBuilder),
            new LowerCaseInteraction(passwordBuilder),
            new UpperCaseInteraction(passwordBuilder),
            new SymbolicInteraction(passwordBuilder),
            new CountInteraction(passwordBuilder)
        );

        return interactions.Values!;
    }
}