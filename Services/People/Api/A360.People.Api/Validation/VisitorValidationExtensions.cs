using A360.People.Api.Contracts;

namespace A360.People.Api.Validation;

public static class VisitorValidationExtensions
{
    public static Dictionary<string, string[]> Validate(
        this CreateVisitorRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.Firstname))
        {
            errors["Firstname"] =
                ["Firstname is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Lastname))
        {
            errors["Lastname"] =
                ["Lastname is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Email))
        {
            errors["Email"] =
                ["Email is required"];
        }

        if (request.StartDate == default)
        {
            errors["StartDate"] =
                ["StartDate is required"];
        }

        if (request.EndDate == default)
        {
            errors["EndDate"] =
                ["EndDate is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(
        this UpdateVisitorRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.Firstname))
        {
            errors["Firstname"] =
                ["Firstname is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Lastname))
        {
            errors["Lastname"] =
                ["Lastname is required"];
        }

        if (string.IsNullOrWhiteSpace(request.Email))
        {
            errors["Email"] =
                ["Email is required"];
        }

        if (request.StartDate == default)
        {
            errors["StartDate"] =
                ["StartDate is required"];
        }

        if (request.EndDate == default)
        {
            errors["EndDate"] =
                ["EndDate is required"];
        }

        return errors;
    }
}