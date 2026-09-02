using A360.Asset.Api.Contracts;

namespace A360.Asset.Api.Validation;

public static class AssetDomainValidationExtensions
{
    public static Dictionary<string, string[]> Validate(this CreateAssetDomainRequest request)
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

        if (string.IsNullOrWhiteSpace(request.FieldName))
        {
            errors["FieldName"] = ["FieldName is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(this UpdateAssetDomainRequest request)
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

        if (string.IsNullOrWhiteSpace(request.FieldName))
        {
            errors["FieldName"] = ["FieldName is required"];
        }

        return errors;
    }
}
