using A360.People.Api.Contracts;

namespace A360.People.Api.Validation;

public static class PersonalVisionAccessValidationExtensions
{
    public static Dictionary<string, string[]> Validate(
        this CreatePersonalVisionAccessRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.GroupName))
        {
            errors["GroupName"] = ["Group Name is required"];
        }

        if (string.IsNullOrWhiteSpace(request.GroupType))
        {
            errors["GroupType"] = ["Group Type is required"];
        }

        if (request.Member is null || !request.Member.Any())
        {
            errors["Member"] = ["At least one member is required"];
        }

        if (request.Reader is null || !request.Reader.Any())
        {
            errors["Reader"] = ["At least one reader is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(
        this UpdatePersonalVisionAccessRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.GroupName))
        {
            errors["GroupName"] = ["Group Name is required"];
        }

        if (string.IsNullOrWhiteSpace(request.GroupType))
        {
            errors["GroupType"] = ["Group Type is required"];
        }

        return errors;
    }
}