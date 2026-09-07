using A360.MasterManagement.Api.Contracts;

namespace A360.MasterManagement.Api.Validation;

public static class DepartmentValidationExtensions
{
    public static Dictionary<string, string[]> Validate(this CreateDepartmentRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.DepartmentName))
        {
            errors["DepartmentName"] = ["DepartmentName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.BusinessUnit))
        {
            errors["BusinessUnit"] = ["BusinessUnit is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(this UpdateDepartmentRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.DepartmentName))
        {
            errors["DepartmentName"] = ["DepartmentName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.BusinessUnit))
        {
            errors["BusinessUnit"] = ["BusinessUnit is required"];
        }

        return errors;
    }
}
