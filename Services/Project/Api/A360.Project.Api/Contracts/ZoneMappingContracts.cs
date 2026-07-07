using System.Text.Json;
using ZoneMappingEntity = A360.Project.Domain.Entities.ZoneMapping;

namespace A360.Project.Api.Contracts;

public sealed record CreateZoneMappingRequest(
    string? ProjectId,
    string? CountryId,
    string? AreaId,
    string? BuildingId,
    string? FloorId,
    string? ZoneId,
    string? ZoneName,
    string? Description,
    string? TopZone,
    string? Priority,
    bool? AssemblyPoint,
    string? Exit,
    bool Status,
    string? CreatedBy,
    string? ClientId,
    string? ZoneColour,
    List<JsonElement>? GeoJsonData);

public sealed record UpdateZoneMappingRequest(
    string? ZoneName,
    string? Description,
    string? TopZone,
    string? Priority,
    bool? AssemblyPoint,
    string? Exit,
    bool Status,
    string? ZoneColour,
    List<JsonElement>? GeoJsonData);

public sealed record ZoneMappingResponse(
    string Id,
    string ProjectId,
    string CountryId,
    string AreaId,
    string BuildingId,
    string FloorId,
    string ZoneId,
    string ZoneName,
    string? Description,
    string? TopZone,
    string? Priority,
    bool? AssemblyPoint,
    string? Exit,
    bool Status,
    string CreatedBy,
    DateTime CreatedAt,
    string ClientId,
    string? ZoneColour,
    List<JsonElement> GeoJsonData)
{
    public static ZoneMappingResponse FromEntity(ZoneMappingEntity zoneMapping)
    {
        return new ZoneMappingResponse(
            zoneMapping.Id,
            zoneMapping.ProjectId,
            zoneMapping.CountryId,
            zoneMapping.AreaId,
            zoneMapping.BuildingId,
            zoneMapping.FloorId,
            zoneMapping.ZoneId,
            zoneMapping.ZoneName,
            zoneMapping.Description,
            zoneMapping.TopZone,
            zoneMapping.Priority,
            zoneMapping.AssemblyPoint,
            zoneMapping.Exit,
            zoneMapping.Status,
            zoneMapping.CreatedBy,
            zoneMapping.CreatedAt,
            zoneMapping.ClientId,
            zoneMapping.ZoneColour,
            GeoJsonConversion.ToJsonElements(zoneMapping.GeoJsonData));
    }
}
