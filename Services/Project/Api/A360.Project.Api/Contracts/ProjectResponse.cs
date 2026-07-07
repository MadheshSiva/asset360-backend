using ProjectEntity = A360.Project.Domain.Entities.Project;

namespace A360.Project.Api.Contracts;

public sealed record ProjectResponse(
    string Id,
    string ProjectName,
    string Description,
    bool Status,
    string CreatedBy,
    DateTime CreatedAt,
    string ClientId,
    DateTime WeekStart,
    DateTime WeekEnd)
{
    public static ProjectResponse FromEntity(ProjectEntity project)
    {
        return new ProjectResponse(
            project.Id,
            project.ProjectName,
            project.Description,
            project.Status,
            project.CreatedBy,
            project.CreatedAt,
            project.ClientId,
            project.WeekStart,
            project.WeekEnd);
    }
}
