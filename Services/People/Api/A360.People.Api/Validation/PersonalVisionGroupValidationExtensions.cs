using A360.People.Api.Contracts;

namespace A360.People.Api.Validation;

public static class PersonalVisionGroupValidationExtensions
{
    public static Dictionary<string, string[]> Validate(
        this CreatePersonalVisionGroupRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.ClientId))
        {
            errors["ClientId"] =
                ["ClientId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.UserId))
        {
            errors["UserId"] =
                ["UserId is required"];
        }

        if (string.IsNullOrWhiteSpace(request.GroupType))
        {
            errors["GroupType"] =
                ["GroupType is required"];
        }

        if (string.IsNullOrWhiteSpace(request.GroupName))
        {
            errors["GroupName"] =
                ["GroupName is required"];
        }

        if (request.Members is null || request.Members.Count == 0)
        {
            errors["Members"] =
                ["At least one member is required"];
        }

        return errors;
    }

    public static Dictionary<string, string[]> Validate(
        this UpdatePersonalVisionGroupRequest request)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(request.GroupType))
        {
            errors["GroupType"] =
                ["GroupType is required"];
        }

        if (string.IsNullOrWhiteSpace(request.GroupName))
        {
            errors["GroupName"] =
                ["GroupName is required"];
        }

        if (request.Members is null || request.Members.Count == 0)
        {
            errors["Members"] =
                ["At least one member is required"];
        }

        return errors;
    }
}