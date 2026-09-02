using A360.MasterManagement.Api.Contracts;

namespace A360.MasterManagement.Api.Validation;

public static class AssetTypeValidationExtensions
{
    public static Dictionary<string, string[]> Validate(this CreateAssetTypeRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.AssetTypeName))
        {
            errors["AssetTypeName"] = ["AssetTypeName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.AssetTypeCode))
        {
            errors["AssetTypeCode"] = ["AssetTypeCode is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(this UpdateAssetTypeRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.AssetTypeName))
        {
            errors["AssetTypeName"] = ["AssetTypeName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.AssetTypeCode))
        {
            errors["AssetTypeCode"] = ["AssetTypeCode is required"];
        }

        return errors;
    }
}
