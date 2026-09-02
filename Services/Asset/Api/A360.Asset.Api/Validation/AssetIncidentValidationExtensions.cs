using A360.Asset.Api.Contracts;

namespace A360.Asset.Api.Validation;

public static class AssetIncidentValidationExtensions
{
    public static Dictionary<string, string[]> Validate(this CreateAssetIncidentRequest request)
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

        if (string.IsNullOrWhiteSpace(request.AlertType))
        {
            errors["AlertType"] = ["AlertType is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(this UpdateAssetIncidentRequest request)
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

        if (string.IsNullOrWhiteSpace(request.AlertType))
        {
            errors["AlertType"] = ["AlertType is required"];
        }

        return errors;
    }
}
