using P360.People.Api.Contracts;

namespace P360.People.Api.Validation;

public static class AccessValidationExtensions
{
    public static Dictionary<string, string[]> Validate(
        this CreateAccessRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.GroupType))
        {
            errors["GroupType"] =
                ["Group type is required"];
        }

        if (string.IsNullOrWhiteSpace(request.GroupName))
        {
            errors["GroupName"] =
                ["Group name is required"];
        }

        if (request.Members is null || request.Members.Count == 0)
        {
            errors["Members"] =
                ["At least one member is required"];
        }

        if (request.Readers is null || request.Readers.Count == 0)
        {
            errors["Readers"] =
                ["At least one reader is required"];
        }

        if (request.FromDateTime >= request.ToDateTime)
        {
            errors["ToDateTime"] =
                ["ToDateTime must be greater than FromDateTime"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(
        this UpdateAccessRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.GroupType))
        {
            errors["GroupType"] =
                ["Group type is required"];
        }

        if (string.IsNullOrWhiteSpace(request.GroupName))
        {
            errors["GroupName"] =
                ["Group name is required"];
        }

        if (request.Members is null || request.Members.Count == 0)
        {
            errors["Members"] =
                ["At least one member is required"];
        }

        if (request.Readers is null || request.Readers.Count == 0)
        {
            errors["Readers"] =
                ["At least one reader is required"];
        }

        if (request.FromDateTime >= request.ToDateTime)
        {
            errors["ToDateTime"] =
                ["ToDateTime must be greater than FromDateTime"];
        }

        return errors;
    }
}