using A360.People.Api.Contracts;

namespace A360.People.Api.Validation;

public static class EmployeeValidationExtensions
{
    public static Dictionary<string, string[]> Validate(
        this CreateEmployeeRequest request)
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

        return errors;
    }

    public static Dictionary<string, string[]> Validate(
        this UpdateEmployeeRequest request)
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

        return errors;
    }
}