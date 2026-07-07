using P360.VisitorManagement.Api.Contracts;

namespace P360.VisitorManagement.Api.Validation;

public static class EmailTemplateValidationExtensions
{
    public static Dictionary<string, string[]> Validate(
        this CreateEmailTemplateRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.Name))
        {
            errors["Name"] =
                ["Name is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Subject))
        {
            errors["Subject"] =
                ["Subject is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Body))
        {
            errors["Body"] =
                ["Body is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(
        this UpdateEmailTemplateRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.Subject))
        {
            errors["Subject"] =
                ["Subject is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Body))
        {
            errors["Body"] =
                ["Body is required"];
        }

        return errors;
    }
}
