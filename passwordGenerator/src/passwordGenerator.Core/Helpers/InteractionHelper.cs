using passwordGenerator.Core.Abstractions;
using passwordGenerator.Core.Builder;
using passwordGenerator.Core.Interactions;
using passwordGenerator.Core.Library.DataStructures.Linear.Array;
using passwordGenerator.Core.Views;

namespace passwordGenerator.Core.Helpers;

public static class InteractionHelper
{
    public static IEnumerable<Interaction<PasswordBuilder>> GetInteractions(out PasswordBuilder passwordBuilder)
    {
        passwordBuilder = new();

        DynamicArray<Interaction<PasswordBuilder>> list = new(
            new HelpInteraction(HelpView.Data)
        );

        return list.Values!;
    }
}