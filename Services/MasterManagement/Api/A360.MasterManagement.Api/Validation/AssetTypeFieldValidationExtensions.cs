using A360.MasterManagement.Api.Contracts;

namespace A360.MasterManagement.Api.Validation;

public static class AssetTypeFieldValidationExtensions
{
    public static Dictionary<string, string[]> Validate(this CreateAssetTypeFieldRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.FieldName))
        {
            errors["FieldName"] = ["FieldName is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(this UpdateAssetTypeFieldRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.FieldName))
        {
            errors["FieldName"] = ["FieldName is required"];
        }

        return errors;
    }
}
