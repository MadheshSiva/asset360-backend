using A360.MasterManagement.Api.Contracts;

namespace A360.MasterManagement.Api.Validation;

public static class BusinessUnitValidationExtensions
{
    public static Dictionary<string, string[]> Validate(this CreateBusinessUnitRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.BusinessUnitName))
        {
            errors["BusinessUnitName"] = ["BusinessUnitName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Organization))
        {
            errors["Organization"] = ["Organization is required"];
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

    public static Dictionary<string, string[]> Validate(this UpdateBusinessUnitRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.BusinessUnitName))
        {
            errors["BusinessUnitName"] = ["BusinessUnitName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Organization))
        {
            errors["Organization"] = ["Organization is required"];
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
