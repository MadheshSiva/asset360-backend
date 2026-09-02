using A360.MasterManagement.Api.Contracts;

namespace A360.MasterManagement.Api.Validation;

public static class CostCenterValidationExtensions
{
    public static Dictionary<string, string[]> Validate(this CreateCostCenterRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.CostCenterName))
        {
            errors["CostCenterName"] = ["CostCenterName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.CostCenterCode))
        {
            errors["CostCenterCode"] = ["CostCenterCode is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(this UpdateCostCenterRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.CostCenterName))
        {
            errors["CostCenterName"] = ["CostCenterName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.CostCenterCode))
        {
            errors["CostCenterCode"] = ["CostCenterCode is required"];
        }

        return errors;
    }
}
