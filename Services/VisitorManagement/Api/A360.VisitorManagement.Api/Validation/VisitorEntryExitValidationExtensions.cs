using A360.VisitorManagement.Api.Contracts;

namespace A360.VisitorManagement.Api.Validation;

public static class VisitorEntryExitValidationExtensions
{
    public static Dictionary<string, string[]> Validate(
        this CreateVisitorEntryExitRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.Name))
        {
            errors["Name"] =
                ["Name is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Type))
        {
            errors["Type"] =
                ["Type is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(
        this UpdateVisitorEntryExitRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.Name))
        {
            errors["Name"] =
                ["Name is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Type))
        {
            errors["Type"] =
                ["Type is required"];
        }

        return errors;
    }
}
