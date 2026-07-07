using P360.VisitorManagement.Api.Contracts;

namespace P360.VisitorManagement.Api.Validation;

public static class VisitorGatePassValidationExtensions
{
    public static Dictionary<string, string[]> Validate(
        this CreateVisitorGatePassRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.ContactName))
        {
            errors["ContactName"] =
                ["ContactName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.EmailId))
        {
            errors["EmailId"] =
                ["EmailId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.HostPersonEmail))
        {
            errors["HostPersonEmail"] =
                ["HostPersonEmail is required"];
        }

        if (request.ToDate < request.FromDate)
        {
            errors["ToDate"] =
                ["ToDate must not be earlier than FromDate"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(
        this UpdateVisitorGatePassRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.ContactName))
        {
            errors["ContactName"] =
                ["ContactName is required"];
        }

        if (string.IsNullOrWhiteSpace(request.EmailId))
        {
            errors["EmailId"] =
                ["EmailId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.HostPersonEmail))
        {
            errors["HostPersonEmail"] =
                ["HostPersonEmail is required"];
        }

        if (request.ToDate < request.FromDate)
        {
            errors["ToDate"] =
                ["ToDate must not be earlier than FromDate"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(
        this ApproveGatePassRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.ApproverEmail))
        {
            errors["ApproverEmail"] =
                ["ApproverEmail is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(
        this RejectGatePassRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.ApproverEmail))
        {
            errors["ApproverEmail"] =
                ["ApproverEmail is required"];
        }

        return errors;
    }
}
