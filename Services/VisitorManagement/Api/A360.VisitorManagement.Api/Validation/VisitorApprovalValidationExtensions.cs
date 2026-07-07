using A360.VisitorManagement.Api.Contracts;

namespace A360.VisitorManagement.Api.Validation;

public static class VisitorApprovalValidationExtensions
{
    public static Dictionary<string, string[]> Validate(
        this CreateVisitorApprovalRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.PermitType))
        {
            errors["PermitType"] =
                ["PermitType is required"];
        }

        ValidateEmployeeEmailIds(request.EmployeeEmailIds, errors);

        return errors;
    }

    public static Dictionary<string, string[]> Validate(
        this UpdateVisitorApprovalRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.PermitType))
        {
            errors["PermitType"] =
                ["PermitType is required"];
        }

        ValidateEmployeeEmailIds(request.EmployeeEmailIds, errors);

        return errors;
    }

    private static void ValidateEmployeeEmailIds(
        List<string>? employeeEmailIds,
        Dictionary<string, string[]> errors)
    {
        if (employeeEmailIds is null || employeeEmailIds.Count == 0)
        {
            errors["EmployeeEmailIds"] =
                ["At least one employee email is required"];
            return;
        }

        if (employeeEmailIds.Any(string.IsNullOrWhiteSpace))
        {
            errors["EmployeeEmailIds"] =
                ["Employee emails cannot be empty"];
        }
    }
}
