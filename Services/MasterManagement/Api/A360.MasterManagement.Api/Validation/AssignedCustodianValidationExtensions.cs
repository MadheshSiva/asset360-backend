using A360.MasterManagement.Api.Contracts;

namespace A360.MasterManagement.Api.Validation;

public static class AssignedCustodianValidationExtensions
{
    public static Dictionary<string, string[]> Validate(this CreateAssignedCustodianRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.DepartmentOrCustodian))
        {
            errors["DepartmentOrCustodian"] = ["DepartmentOrCustodian is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Name))
        {
            errors["Name"] = ["Name is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(this UpdateAssignedCustodianRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.AssetId))
        {
            errors["AssetId"] = ["AssetId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.DepartmentOrCustodian))
        {
            errors["DepartmentOrCustodian"] = ["DepartmentOrCustodian is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Name))
        {
            errors["Name"] = ["Name is required"];
        }

        return errors;
    }
}
