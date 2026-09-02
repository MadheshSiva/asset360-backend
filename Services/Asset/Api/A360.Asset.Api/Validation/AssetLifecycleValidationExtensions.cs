using A360.Asset.Api.Contracts;

namespace A360.Asset.Api.Validation;

public static class AssetLifecycleValidationExtensions
{
    private static readonly string[] AllowedStatuses = ["Active", "Retired"];

    public static Dictionary<string, string[]> Validate(this CreateAssetLifecycleRequest request)
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

        if (!string.IsNullOrWhiteSpace(request.Status) && !AllowedStatuses.Contains(request.Status))
        {
            errors["Status"] = ["Status must be one of: Active, Retired"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(this UpdateAssetLifecycleRequest request)
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

        if (!string.IsNullOrWhiteSpace(request.Status) && !AllowedStatuses.Contains(request.Status))
        {
            errors["Status"] = ["Status must be one of: Active, Retired"];
        }

        return errors;
    }
}
