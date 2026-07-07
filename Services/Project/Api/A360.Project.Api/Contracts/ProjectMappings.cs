using ProjectEntity = A360.Project.Domain.Entities.Project;

namespace A360.Project.Api.Contracts;

internal static class ProjectMappings
{
    public static ProjectEntity ToEntity(this CreateProjectRequest request)
    {
        return new ProjectEntity
        {
            ProjectName = request.ProjectName.Trim(),
            Description = request.Description?.Trim() ?? string.Empty,
            Status = request.Status,
            CreatedBy = request.CreatedBy.Trim(),
            CreatedAt = DateTime.UtcNow,
            ClientId = request.ClientId.Trim(),
            WeekStart = ToUtc(request.WeekStart),
            WeekEnd = ToUtc(request.WeekEnd)
        };
    }

    public static void ApplyTo(this UpdateProjectRequest request, ProjectEntity project)
    {
        project.ProjectName = request.ProjectName.Trim();
        project.Description = request.Description?.Trim() ?? string.Empty;
        project.Status = request.Status;
        project.ClientId = request.ClientId.Trim();
        project.WeekStart = ToUtc(request.WeekStart);
        project.WeekEnd = ToUtc(request.WeekEnd);
    }

    private static DateTime ToUtc(DateTime value)
    {
        return value.Kind switch
        {
            DateTimeKind.Utc => value,
            DateTimeKind.Local => value.ToUniversalTime(),
            _ => DateTime.SpecifyKind(value, DateTimeKind.Utc)
        };
    }
}
