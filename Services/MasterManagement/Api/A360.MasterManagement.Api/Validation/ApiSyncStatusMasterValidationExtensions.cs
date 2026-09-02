using A360.MasterManagement.Api.Contracts;

namespace A360.MasterManagement.Api.Validation;

public static class ApiSyncStatusMasterValidationExtensions
{
    public static Dictionary<string, string[]> Validate(this CreateApiSyncStatusMasterRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.StatusName))
        {
            errors["StatusName"] = ["StatusName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.StatusCode))
        {
            errors["StatusCode"] = ["StatusCode is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(this UpdateApiSyncStatusMasterRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.StatusName))
        {
            errors["StatusName"] = ["StatusName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.StatusCode))
        {
            errors["StatusCode"] = ["StatusCode is required"];
        }

        return errors;
    }
}
