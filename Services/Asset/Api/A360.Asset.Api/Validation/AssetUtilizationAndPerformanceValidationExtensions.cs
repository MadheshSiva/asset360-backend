using A360.Asset.Api.Contracts;

namespace A360.Asset.Api.Validation;

public static class AssetUtilizationAndPerformanceValidationExtensions
{
    public static Dictionary<string, string[]> Validate(this CreateAssetUtilizationAndPerformanceRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.AssetName))
        {
            errors["AssetName"] = ["AssetName is required"];
        }

        if (request.UtilizationPercentage is < 0 or > 100)
        {
            errors["UtilizationPercentage"] = ["UtilizationPercentage must be between 0 and 100"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(this UpdateAssetUtilizationAndPerformanceRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.AssetName))
        {
            errors["AssetName"] = ["AssetName is required"];
        }

        if (request.UtilizationPercentage is < 0 or > 100)
        {
            errors["UtilizationPercentage"] = ["UtilizationPercentage must be between 0 and 100"];
        }

        return errors;
    }
}
