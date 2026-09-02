using ProjectEntity = A360.Project.Domain.Entities.Project;

namespace A360.Project.Api.Contracts;

public sealed record ProjectResponse(
    string Id,
    string ProjectName,
    string Description,
    bool Status,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted,
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
            project.UpdatedBy,
            project.UpdatedAt,
            project.ClientId,
            project.TenantId,
            project.IsDeleted,
            project.WeekStart,
            project.WeekEnd);
    }
}
