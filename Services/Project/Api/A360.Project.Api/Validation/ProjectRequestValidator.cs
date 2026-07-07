using A360.Project.Api.Contracts;

namespace A360.Project.Api.Validation;

internal static class ProjectRequestValidator
{
    public static IDictionary<string, string[]> Validate(this CreateProjectRequest request)
    {
        var errors = ValidateShared(
            request.ProjectName,
            request.Description,
            request.ClientId,
            request.WeekStart,
            request.WeekEnd);

        if (string.IsNullOrWhiteSpace(request.CreatedBy))
        {
            errors.Add(nameof(request.CreatedBy), ["Created by is required."]);
        }

        return errors;
    }

    public static IDictionary<string, string[]> Validate(this UpdateProjectRequest request)
    {
        return ValidateShared(
            request.ProjectName,
            request.Description,
            request.ClientId,
            request.WeekStart,
            request.WeekEnd);
    }

    private static Dictionary<string, string[]> ValidateShared(
        string projectName,
        string description,
        string clientId,
        DateTime weekStart,
        DateTime weekEnd)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(projectName))
        {
            errors.Add(nameof(CreateProjectRequest.ProjectName), ["Project name is required."]);
        }

        if (description is null)
        {
            errors.Add(nameof(CreateProjectRequest.Description), ["Description cannot be null."]);
        }

        if (string.IsNullOrWhiteSpace(clientId))
        {
            errors.Add(nameof(CreateProjectRequest.ClientId), ["Client id is required."]);
        }

        if (weekStart == default)
        {
            errors.Add(nameof(CreateProjectRequest.WeekStart), ["Week start is required."]);
        }

        if (weekEnd == default)
        {
            errors.Add(nameof(CreateProjectRequest.WeekEnd), ["Week end is required."]);
        }

        if (weekStart != default && weekEnd != default && weekEnd < weekStart)
        {
            errors.Add(nameof(CreateProjectRequest.WeekEnd), ["Week end must be on or after week start."]);
        }

        return errors;
    }
}
