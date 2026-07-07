using ZoneEntity = P360.Project.Domain.Entities.Zone;

namespace P360.Project.Api.Contracts;

public sealed record CreateZoneRequest(
    string? ProjectId,
    string? CountryId,
    string? AreaId,
    string? BuildingId,
    string? FloorId,
    string? ZoneName,
    string? Description,
    string? TopZone,
    string? Priority,
    bool MusterPoint,
    bool ExitPoint,
    bool Status,
    string? CreatedBy,
    string? ClientId,
    int? TimeTakenAssemblePoint,
    string? MapPath);

public sealed record UpdateZoneRequest(
    string? ZoneName,
    string? Description,
    string? TopZone,
    string? Priority,
    bool MusterPoint,
    bool ExitPoint,
    bool Status,
    int? TimeTakenAssemblePoint,
    string? MapPath);

public sealed record ZoneResponse(
    string Id,
    string ProjectId,
    string CountryId,
    string AreaId,
    string BuildingId,
    string FloorId,
    string ZoneName,
    string Description,
    string TopZone,
    string Priority,
    bool MusterPoint,
    bool ExitPoint,
    bool Status,
    string CreatedBy,
    DateTime CreatedAt,
    string ClientId,
    int? TimeTakenAssemblePoint,
    string MapPath)
{
    public static ZoneResponse FromEntity(ZoneEntity zone)
    {
        return new ZoneResponse(
            zone.Id,
            zone.ProjectId,
            zone.CountryId,
            zone.AreaId,
            zone.BuildingId,
            zone.FloorId,
            zone.ZoneName,
            zone.Description,
            zone.TopZone,
            zone.Priority,
            zone.MusterPoint,
            zone.ExitPoint,
            zone.Status,
            zone.CreatedBy,
            zone.CreatedAt,
            zone.ClientId,
            zone.TimeTakenAssemblePoint,
            zone.MapPath);
    }
}

public sealed record ZoneMapResponse(string Id, string MapPath);
