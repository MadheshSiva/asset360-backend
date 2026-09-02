using A360.MasterManagement.Api.Contracts;

namespace A360.MasterManagement.Api.Validation;

public static class PermissionMasterValidationExtensions
{
    public static Dictionary<string, string[]> Validate(this CreatePermissionMasterRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.PermissionName))
        {
            errors["PermissionName"] = ["PermissionName is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(this UpdatePermissionMasterRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.PermissionName))
        {
            errors["PermissionName"] = ["PermissionName is required"];
        }

        return errors;
    }
}
