using A360.MasterManagement.Api.Contracts;

namespace A360.MasterManagement.Api.Validation;

public static class UnitMasterValidationExtensions
{
    public static Dictionary<string, string[]> Validate(this CreateUnitMasterRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.UnitName))
        {
            errors["UnitName"] = ["UnitName is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(this UpdateUnitMasterRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.UnitName))
        {
            errors["UnitName"] = ["UnitName is required"];
        }

        return errors;
    }
}
