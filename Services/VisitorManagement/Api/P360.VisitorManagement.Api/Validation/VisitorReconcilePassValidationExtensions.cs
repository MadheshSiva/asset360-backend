using P360.VisitorManagement.Api.Contracts;

namespace P360.VisitorManagement.Api.Validation;

public static class VisitorReconcilePassValidationExtensions
{
    public static Dictionary<string, string[]> Validate(
        this CreateVisitorReconcilePassRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.NumberOfVisitors))
        {
            errors["NumberOfVisitors"] =
                ["NumberOfVisitors is required"];
        }

        if (string.IsNullOrWhiteSpace(request.NumberOfPeopleExited))
        {
            errors["NumberOfPeopleExited"] =
                ["NumberOfPeopleExited is required"];
        }

        if (string.IsNullOrWhiteSpace(request.VisitorPhysicallyPresent))
        {
            errors["VisitorPhysicallyPresent"] =
                ["VisitorPhysicallyPresent is required"];
        }

        if (string.IsNullOrWhiteSpace(request.VerifiedSecurityEmpNo))
        {
            errors["VerifiedSecurityEmpNo"] =
                ["VerifiedSecurityEmpNo is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(
        this UpdateVisitorReconcilePassRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.NumberOfVisitors))
        {
            errors["NumberOfVisitors"] =
                ["NumberOfVisitors is required"];
        }

        if (string.IsNullOrWhiteSpace(request.NumberOfPeopleExited))
        {
            errors["NumberOfPeopleExited"] =
                ["NumberOfPeopleExited is required"];
        }

        if (string.IsNullOrWhiteSpace(request.VisitorPhysicallyPresent))
        {
            errors["VisitorPhysicallyPresent"] =
                ["VisitorPhysicallyPresent is required"];
        }

        if (string.IsNullOrWhiteSpace(request.VerifiedSecurityEmpNo))
        {
            errors["VerifiedSecurityEmpNo"] =
                ["VerifiedSecurityEmpNo is required"];
        }

        return errors;
    }
}
