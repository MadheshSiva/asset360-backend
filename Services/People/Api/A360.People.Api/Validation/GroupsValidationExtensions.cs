using A360.People.Api.Contracts;

namespace A360.People.Api.Validation;

public static class GroupValidationExtensions
{
    public static Dictionary<string, string[]> Validate(
        this CreateGroupRequest request)
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

        return errors;
    }

    public static Dictionary<string, string[]> Validate(
        this UpdateGroupRequest request)
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

        return errors;
    }
}