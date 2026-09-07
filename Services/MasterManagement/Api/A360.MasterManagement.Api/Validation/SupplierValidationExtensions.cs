using A360.MasterManagement.Api.Contracts;

namespace A360.MasterManagement.Api.Validation;

public static class SupplierValidationExtensions
{
    public static Dictionary<string, string[]> Validate(this CreateSupplierRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.SupplierName))
        {
            errors["SupplierName"] = ["SupplierName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Email))
        {
            errors["Email"] = ["Email is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Phone))
        {
            errors["Phone"] = ["Phone is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(this UpdateSupplierRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.SupplierName))
        {
            errors["SupplierName"] = ["SupplierName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Email))
        {
            errors["Email"] = ["Email is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Phone))
        {
            errors["Phone"] = ["Phone is required"];
        }

        return errors;
    }
}
