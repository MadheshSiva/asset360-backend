using System.Text.Json;
using DeviceZoneMappingEntity = A360.Project.Domain.Entities.DeviceZoneMapping;

namespace A360.Project.Api.Contracts;

public sealed record CreateDeviceZoneMappingRequest(
    string? ProjectId,
    string? CountryId,
    string? AreaId,
    string? BuildingId,
    string? FloorId,
    string? ZoneId,
    string? ZoneName,
    string? DeviceReferenceId,
    string? DeviceName,
    string? Description,
    string? TopZone,
    string? Priority,
    string? AssemblyPoint,
    string? Exit,
    bool Status,
    string? CreatedBy,
    string? ClientId,
    List<JsonElement>? DeviceGeoJsonData);

public sealed record UpdateDeviceZoneMappingRequest(
    string? DeviceName,
    string? Description,
    string? TopZone,
    string? Priority,
    string? AssemblyPoint,
    string? Exit,
    bool Status,
    List<JsonElement>? DeviceGeoJsonData);

public sealed record DeviceZoneMappingResponse(
    string Id,
    string ProjectId,
    string CountryId,
    string AreaId,
    string BuildingId,
    string FloorId,
    string ZoneId,
    string ZoneName,
    string DeviceReferenceId,
    string DeviceName,
    string? Description,
    string? TopZone,
    string? Priority,
    string? AssemblyPoint,
    string? Exit,
    bool Status,
    string CreatedBy,
    DateTime CreatedAt,
    string ClientId,
    List<JsonElement> DeviceGeoJsonData)
{
    public static DeviceZoneMappingResponse FromEntity(DeviceZoneMappingEntity deviceZoneMapping)
    {
        return new DeviceZoneMappingResponse(
            deviceZoneMapping.Id,
            deviceZoneMapping.ProjectId,
            deviceZoneMapping.CountryId,
            deviceZoneMapping.AreaId,
            deviceZoneMapping.BuildingId,
            deviceZoneMapping.FloorId,
            deviceZoneMapping.ZoneId,
            deviceZoneMapping.ZoneName,
            deviceZoneMapping.DeviceReferenceId,
            deviceZoneMapping.DeviceName,
            deviceZoneMapping.Description,
            deviceZoneMapping.TopZone,
            deviceZoneMapping.Priority,
            deviceZoneMapping.AssemblyPoint,
            deviceZoneMapping.Exit,
            deviceZoneMapping.Status,
            deviceZoneMapping.CreatedBy,
            deviceZoneMapping.CreatedAt,
            deviceZoneMapping.ClientId,
            GeoJsonConversion.ToJsonElements(deviceZoneMapping.DeviceGeoJsonData));
    }
}
