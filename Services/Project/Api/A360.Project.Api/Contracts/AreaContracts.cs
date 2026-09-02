using AreaEntity = A360.Project.Domain.Entities.Area;

namespace A360.Project.Api.Contracts;

public sealed record CreateAreaRequest(
    string? ProjectId,
    string? CountryId,
    string? AreaName,
    string? Description,
    string? OutlineMap,
    string? Latitude,
    string? Longitude,
    bool Status,
    string? CreatedBy,
    string? ClientId,
    string? TenantId,
    string? MapPath);

public sealed record UpdateAreaRequest(
    string? AreaName,
    string? Description,
    string? OutlineMap,
    string? Latitude,
    string? Longitude,
    bool Status,
    string? MapPath,
    string? UpdatedBy);

public sealed record AreaResponse(
    string Id,
    string ProjectId,
    string CountryId,
    string AreaName,
    string Description,
    string OutlineMap,
    string Latitude,
    string Longitude,
    bool Status,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted,
    string MapPath)
{
    public static AreaResponse FromEntity(AreaEntity area)
    {
        return new AreaResponse(
            area.Id,
            area.ProjectId,
            area.CountryId,
            area.AreaName,
            area.Description,
            area.OutlineMap,
            area.Latitude,
            area.Longitude,
            area.Status,
            area.CreatedBy,
            area.CreatedAt,
            area.UpdatedBy,
            area.UpdatedAt,
            area.ClientId,
            area.TenantId,
            area.IsDeleted,
            area.MapPath);
    }
}
