using A360.MasterManagement.Api.Contracts;

namespace A360.MasterManagement.Api.Validation;

public static class SeverityMasterValidationExtensions
{
    public static Dictionary<string, string[]> Validate(this CreateSeverityMasterRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.SeverityName))
        {
            errors["SeverityName"] = ["SeverityName is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(this UpdateSeverityMasterRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.SeverityName))
        {
            errors["SeverityName"] = ["SeverityName is required"];
        }

        return errors;
    }
}
