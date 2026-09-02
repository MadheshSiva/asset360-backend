using A360.Asset.Api.Contracts;

namespace A360.Asset.Api.Validation;

public static class TaggedAssetsValidationExtensions
{
    public static Dictionary<string, string[]> Validate(this CreateTaggedAssetsRequest request)
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

    public static Dictionary<string, string[]> Validate(this UpdateTaggedAssetsRequest request)
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
}
