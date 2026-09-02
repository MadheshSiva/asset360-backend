using A360.MasterManagement.Api.Contracts;

namespace A360.MasterManagement.Api.Validation;

public static class PermitTypeMasterValidationExtensions
{
    public static Dictionary<string, string[]> Validate(this CreatePermitTypeMasterRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.PermitName))
        {
            errors["PermitName"] = ["PermitName is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(this UpdatePermitTypeMasterRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.PermitName))
        {
            errors["PermitName"] = ["PermitName is required"];
        }

        return errors;
    }
}
