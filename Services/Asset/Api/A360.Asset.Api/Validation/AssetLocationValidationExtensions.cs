using A360.Asset.Api.Contracts;

namespace A360.Asset.Api.Validation;

public static class AssetLocationValidationExtensions
{
    public static Dictionary<string, string[]> Validate(this CreateAssetLocationRequest request)
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

        return errors;
    }

    public static Dictionary<string, string[]> Validate(this UpdateAssetLocationRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetName))
        {
            errors["AssetName"] = ["AssetName is required"];
        }

        return errors;
    }
}
