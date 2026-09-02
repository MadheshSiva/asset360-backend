using A360.Asset.Api.Contracts;

namespace A360.Asset.Api.Validation;

public static class AssetValidationExtensions
{
    public static Dictionary<string, string[]> Validate(this CreateAssetRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetName))
        {
            errors["AssetName"] = ["AssetName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.CategorySubCategory))
        {
            errors["CategorySubCategory"] = ["CategorySubCategory is required"];
        }

        if (string.IsNullOrWhiteSpace(request.SerialNumber))
        {
            errors["SerialNumber"] = ["SerialNumber is required"];
        }

        if (string.IsNullOrWhiteSpace(request.AssetType))
        {
            errors["AssetType"] = ["AssetType is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(this UpdateAssetRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetName))
        {
            errors["AssetName"] = ["AssetName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.CategorySubCategory))
        {
            errors["CategorySubCategory"] = ["CategorySubCategory is required"];
        }

        if (string.IsNullOrWhiteSpace(request.SerialNumber))
        {
            errors["SerialNumber"] = ["SerialNumber is required"];
        }

        if (string.IsNullOrWhiteSpace(request.AssetType))
        {
            errors["AssetType"] = ["AssetType is required"];
        }

        return errors;
    }
}
