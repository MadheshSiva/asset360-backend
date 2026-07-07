using P360.VisitorManagement.Api.Contracts;

namespace P360.VisitorManagement.Api.Validation;

public static class VisitorRegistrationValidationExtensions
{
    public static Dictionary<string, string[]> Validate(
        this CreateVisitorRegistrationRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.VisitorType))
        {
            errors["VisitorType"] =
                ["VisitorType is required"];
        }

        if (string.IsNullOrWhiteSpace(request.FirstName))
        {
            errors["FirstName"] =
                ["FirstName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.LastName))
        {
            errors["LastName"] =
                ["LastName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Email))
        {
            errors["Email"] =
                ["Email is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(
        this UpdateVisitorRegistrationRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.VisitorType))
        {
            errors["VisitorType"] =
                ["VisitorType is required"];
        }

        if (string.IsNullOrWhiteSpace(request.FirstName))
        {
            errors["FirstName"] =
                ["FirstName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.LastName))
        {
            errors["LastName"] =
                ["LastName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Email))
        {
            errors["Email"] =
                ["Email is required"];
        }

        return errors;
    }
}
