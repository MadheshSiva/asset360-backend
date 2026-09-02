namespace A360.Project.Api.Contracts;

public sealed record UpdateProjectRequest(
    string ProjectName,
    string Description,
    bool Status,
    string ClientId,
    DateTime WeekStart,
    DateTime WeekEnd,
    string? UpdatedBy);
