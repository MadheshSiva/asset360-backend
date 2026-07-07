using A360.VisitorManagement.Api.Contracts;

namespace A360.VisitorManagement.Api.Validation;

public static class VisitorIdentificationValidationExtensions
{
    public static Dictionary<string, string[]> Validate(
        this CreateVisitorIdentificationRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.IdentificationType))
        {
            errors["IdentificationType"] =
                ["IdentificationType is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Name))
        {
            errors["Name"] =
                ["Name is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(
        this UpdateVisitorIdentificationRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.IdentificationType))
        {
            errors["IdentificationType"] =
                ["IdentificationType is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Name))
        {
            errors["Name"] =
                ["Name is required"];
        }

        return errors;
    }
}
