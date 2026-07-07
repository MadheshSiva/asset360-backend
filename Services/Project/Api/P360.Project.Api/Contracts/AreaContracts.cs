using AreaEntity = P360.Project.Domain.Entities.Area;

namespace P360.Project.Api.Contracts;

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
    string? MapPath);

public sealed record UpdateAreaRequest(
    string? AreaName,
    string? Description,
    string? OutlineMap,
    string? Latitude,
    string? Longitude,
    bool Status,
    string? MapPath);

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
    string CreatedBy,
    DateTime CreatedAt,
    string ClientId,
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
            area.ClientId,
            area.MapPath);
    }
}
