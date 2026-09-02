using A360.Asset.Api.Contracts;

namespace A360.Asset.Api.Validation;

public static class AssetFinancialDetailsValidationExtensions
{
    public static Dictionary<string, string[]> Validate(this CreateAssetFinancialDetailsRequest request)
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

        if (request.PurchaseCost < 0)
        {
            errors["PurchaseCost"] = ["PurchaseCost cannot be negative"];
        }

        if (request.CurrentBookValue < 0)
        {
            errors["CurrentBookValue"] = ["CurrentBookValue cannot be negative"];
        }

        if (request.ResidualValue < 0)
        {
            errors["ResidualValue"] = ["ResidualValue cannot be negative"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(this UpdateAssetFinancialDetailsRequest request)
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

        if (request.PurchaseCost < 0)
        {
            errors["PurchaseCost"] = ["PurchaseCost cannot be negative"];
        }

        if (request.CurrentBookValue < 0)
        {
            errors["CurrentBookValue"] = ["CurrentBookValue cannot be negative"];
        }

        if (request.ResidualValue < 0)
        {
            errors["ResidualValue"] = ["ResidualValue cannot be negative"];
        }

        return errors;
    }
}
