using A360.MasterManagement.Api.Contracts;

namespace A360.MasterManagement.Api.Validation;

public static class PriorityValidationExtensions
{
    public static Dictionary<string, string[]> Validate(this CreatePriorityRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.PriorityName))
        {
            errors["PriorityName"] = ["PriorityName is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(this UpdatePriorityRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.PriorityName))
        {
            errors["PriorityName"] = ["PriorityName is required"];
        }

        return errors;
    }
}
