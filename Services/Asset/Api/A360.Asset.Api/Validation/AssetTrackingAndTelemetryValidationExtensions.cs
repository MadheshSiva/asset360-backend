using A360.Asset.Api.Contracts;

namespace A360.Asset.Api.Validation;

public static class AssetTrackingAndTelemetryValidationExtensions
{
    public static Dictionary<string, string[]> Validate(this CreateAssetTrackingAndTelemetryRequest request)
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

        if (string.IsNullOrWhiteSpace(request.DeviceIdentifier))
        {
            errors["DeviceIdentifier"] = ["DeviceIdentifier is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(this UpdateAssetTrackingAndTelemetryRequest request)
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

        if (string.IsNullOrWhiteSpace(request.DeviceIdentifier))
        {
            errors["DeviceIdentifier"] = ["DeviceIdentifier is required"];
        }

        return errors;
    }
}
