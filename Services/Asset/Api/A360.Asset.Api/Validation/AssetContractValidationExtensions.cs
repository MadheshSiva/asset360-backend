using A360.Asset.Api.Contracts;

namespace A360.Asset.Api.Validation;

public static class AssetContractValidationExtensions
{
    public static Dictionary<string, string[]> Validate(this CreateAssetContractRequest request)
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

        if (request.WarrantyEndDate.HasValue && request.WarrantyStartDate.HasValue
            && request.WarrantyEndDate < request.WarrantyStartDate)
        {
            errors["WarrantyEndDate"] = ["WarrantyEndDate cannot be earlier than WarrantyStartDate"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(this UpdateAssetContractRequest request)
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

        if (request.WarrantyEndDate.HasValue && request.WarrantyStartDate.HasValue
            && request.WarrantyEndDate < request.WarrantyStartDate)
        {
            errors["WarrantyEndDate"] = ["WarrantyEndDate cannot be earlier than WarrantyStartDate"];
        }

        return errors;
    }
}
