using A360.MasterManagement.Api.Contracts;

namespace A360.MasterManagement.Api.Validation;

public static class ShiftMasterValidationExtensions
{
    public static Dictionary<string, string[]> Validate(this CreateShiftMasterRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.ShiftName))
        {
            errors["ShiftName"] = ["ShiftName is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(this UpdateShiftMasterRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.ShiftName))
        {
            errors["ShiftName"] = ["ShiftName is required"];
        }

        return errors;
    }
}
