using static passwordGenerator.Core.Helpers.ResultHelper;

namespace passwordGenerator.Core.Shared;

public record Password(string? Data = null, string? Information = null, string? Errors = null)
{
    public void Execute()
    {
        if (!string.IsNullOrEmpty(Data))
        {
            HandleSuccess(Data);
        }

        if (!string.IsNullOrEmpty(Information))
        {
            HandleInformation(Information);
        }

        if (!string.IsNullOrEmpty(Errors))
        {
            HandleError(Errors);
        }
    }
}