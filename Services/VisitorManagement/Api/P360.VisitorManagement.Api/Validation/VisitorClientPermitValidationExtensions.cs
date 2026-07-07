using P360.VisitorManagement.Api.Contracts;

namespace P360.VisitorManagement.Api.Validation;

public static class VisitorClientPermitValidationExtensions
{
    public static Dictionary<string, string[]> Validate(
        this CreateVisitorClientPermitRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.ClientName))
        {
            errors["ClientName"] =
                ["ClientName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.ClientEmail))
        {
            errors["ClientEmail"] =
                ["ClientEmail is required"];
        }

        if (string.IsNullOrWhiteSpace(request.SupportContactNo))
        {
            errors["SupportContactNo"] =
                ["SupportContactNo is required"];
        }

        if (string.IsNullOrWhiteSpace(request.SecurityContactNo))
        {
            errors["SecurityContactNo"] =
                ["SecurityContactNo is required"];
        }

        if (string.IsNullOrWhiteSpace(request.FireContactNo))
        {
            errors["FireContactNo"] =
                ["FireContactNo is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(
        this UpdateVisitorClientPermitRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.ClientName))
        {
            errors["ClientName"] =
                ["ClientName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.ClientEmail))
        {
            errors["ClientEmail"] =
                ["ClientEmail is required"];
        }

        if (string.IsNullOrWhiteSpace(request.SupportContactNo))
        {
            errors["SupportContactNo"] =
                ["SupportContactNo is required"];
        }

        if (string.IsNullOrWhiteSpace(request.SecurityContactNo))
        {
            errors["SecurityContactNo"] =
                ["SecurityContactNo is required"];
        }

        if (string.IsNullOrWhiteSpace(request.FireContactNo))
        {
            errors["FireContactNo"] =
                ["FireContactNo is required"];
        }

        return errors;
    }
}
