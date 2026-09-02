using A360.MasterManagement.Api.Contracts;

namespace A360.MasterManagement.Api.Validation;

public static class ConditionMasterValidationExtensions
{
    public static Dictionary<string, string[]> Validate(this CreateConditionMasterRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.ConditionName))
        {
            errors["ConditionName"] = ["ConditionName is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(this UpdateConditionMasterRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.ConditionName))
        {
            errors["ConditionName"] = ["ConditionName is required"];
        }

        return errors;
    }
}
