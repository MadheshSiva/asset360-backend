using OuterZoneEntity = A360.Project.Domain.Entities.OuterZone;

namespace A360.Project.Api.Contracts;

public sealed record CreateOuterZoneRequest(
    string? ProjectId,
    string? CountryId,
    string? AreaId,
    string? OuterZoneName,
    string? Description,
    string? OutlineMap,
    string? Latitude,
    string? Longitude,
    bool Status,
    string? CreatedBy,
    string? ClientId,
    string? TenantId,
    string? MapPath);

public sealed record UpdateOuterZoneRequest(
    string? OuterZoneName,
    string? Description,
    string? OutlineMap,
    string? Latitude,
    string? Longitude,
    bool Status,
    string? MapPath,
    string? UpdatedBy);

public sealed record OuterZoneResponse(
    string Id,
    string ProjectId,
    string CountryId,
    string AreaId,
    string OuterZoneName,
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
    public static OuterZoneResponse FromEntity(OuterZoneEntity outerZone)
    {
        return new OuterZoneResponse(
            outerZone.Id,
            outerZone.ProjectId,
            outerZone.CountryId,
            outerZone.AreaId,
            outerZone.OuterZoneName,
            outerZone.Description,
            outerZone.OutlineMap,
            outerZone.Latitude,
            outerZone.Longitude,
            outerZone.Status,
            outerZone.CreatedBy,
            outerZone.CreatedAt,
            outerZone.UpdatedBy,
            outerZone.UpdatedAt,
            outerZone.ClientId,
            outerZone.TenantId,
            outerZone.IsDeleted,
            outerZone.MapPath);
    }
}
