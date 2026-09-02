using SubZoneEntity = A360.Project.Domain.Entities.SubZone;

namespace A360.Project.Api.Contracts;

public sealed record CreateSubZoneRequest(
    string? ProjectId,
    string? CountryId,
    string? AreaId,
    string? OuterZoneId,
    string? BuildingId,
    string? FloorId,
    string? ZoneId,
    string? SubZoneName,
    string? Description,
    bool TopZone,
    int Priority,
    bool AssemblyPoint,
    bool Exit,
    bool Status,
    string? CreatedBy,
    string? ClientId,
    string? TenantId,
    int? TimeTakenAssemblePoint,
    string? MapPath);

public sealed record UpdateSubZoneRequest(
    string? SubZoneName,
    string? Description,
    bool TopZone,
    int Priority,
    bool AssemblyPoint,
    bool Exit,
    bool Status,
    int? TimeTakenAssemblePoint,
    string? MapPath,
    string? UpdatedBy);

public sealed record SubZoneResponse(
    string Id,
    string ProjectId,
    string CountryId,
    string AreaId,
    string OuterZoneId,
    string BuildingId,
    string FloorId,
    string ZoneId,
    string SubZoneName,
    string Description,
    string TopZone,
    string Priority,
    bool AssemblyPoint,
    string Exit,
    bool Status,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted,
    string TimeTakenAssemblePoint,
    string MapPath)
{
    public static SubZoneResponse FromEntity(SubZoneEntity subZone)
    {
        return new SubZoneResponse(
            subZone.Id,
            subZone.ProjectId,
            subZone.CountryId,
            subZone.AreaId,
            subZone.OuterZoneId,
            subZone.BuildingId,
            subZone.FloorId,
            subZone.ZoneId,
            subZone.SubZoneName,
            subZone.Description,
            subZone.TopZone,
            subZone.Priority,
            subZone.AssemblyPoint,
            subZone.Exit,
            subZone.Status,
            subZone.CreatedBy,
            subZone.CreatedAt,
            subZone.UpdatedBy,
            subZone.UpdatedAt,
            subZone.ClientId,
            subZone.TenantId,
            subZone.IsDeleted,
            subZone.TimeTakenAssemblePoint,
            subZone.MapPath);
    }
}

public sealed record SubZoneMapResponse(string Id, string MapPath);
