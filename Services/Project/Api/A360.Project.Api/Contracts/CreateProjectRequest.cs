namespace A360.Project.Api.Contracts;

public sealed record CreateProjectRequest(
    string ProjectName,
    string Description,
    bool Status,
    string CreatedBy,
    string ClientId,
    DateTime WeekStart,
    DateTime WeekEnd,
    string? TenantId);
