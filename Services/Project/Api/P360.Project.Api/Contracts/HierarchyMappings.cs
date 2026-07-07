using AreaEntity = P360.Project.Domain.Entities.Area;
using BuildingEntity = P360.Project.Domain.Entities.Building;
using CountryEntity = P360.Project.Domain.Entities.Country;
using DeviceZoneMappingEntity = P360.Project.Domain.Entities.DeviceZoneMapping;
using FloorEntity = P360.Project.Domain.Entities.Floor;
using SubZoneEntity = P360.Project.Domain.Entities.SubZone;
using ZoneEntity = P360.Project.Domain.Entities.Zone;
using ZoneMappingEntity = P360.Project.Domain.Entities.ZoneMapping;

namespace P360.Project.Api.Contracts;

internal static class HierarchyMappings
{
    public static CountryEntity ToEntity(this CreateCountryRequest request)
    {
        return new CountryEntity
        {
            ProjectId = Clean(request.ProjectId),
            CountryName = Clean(request.CountryName),
            Description = Clean(request.Description),
            TimeZone = Clean(request.TimeZone),
            CountryCode = Clean(request.CountryCode),
            Latitude = Clean(request.Latitude),
            Longitude = Clean(request.Longitude),
            Status = request.Status,
            CreatedBy = Clean(request.CreatedBy),
            CreatedAt = DateTime.UtcNow,
            ClientId = Clean(request.ClientId)
        };
    }

    public static void ApplyTo(this UpdateCountryRequest request, CountryEntity country)
    {
        country.CountryName = Clean(request.CountryName);
        country.Description = Clean(request.Description);
        country.TimeZone = Clean(request.TimeZone);
        country.CountryCode = Clean(request.CountryCode);
        country.Latitude = Clean(request.Latitude);
        country.Longitude = Clean(request.Longitude);
        country.Status = request.Status;
        country.ClientId = Clean(request.ClientId);
    }

    public static AreaEntity ToEntity(this CreateAreaRequest request)
    {
        return new AreaEntity
        {
            ProjectId = Clean(request.ProjectId),
            CountryId = Clean(request.CountryId),
            AreaName = Clean(request.AreaName),
            Description = Clean(request.Description),
            OutlineMap = Clean(request.OutlineMap),
            Latitude = Clean(request.Latitude),
            Longitude = Clean(request.Longitude),
            Status = request.Status,
            CreatedBy = Clean(request.CreatedBy),
            CreatedAt = DateTime.UtcNow,
            ClientId = Clean(request.ClientId),
            MapPath = Clean(request.MapPath)
        };
    }

    public static void ApplyTo(this UpdateAreaRequest request, AreaEntity area)
    {
        area.AreaName = Clean(request.AreaName);
        area.Description = Clean(request.Description);
        area.OutlineMap = Clean(request.OutlineMap);
        area.Latitude = Clean(request.Latitude);
        area.Longitude = Clean(request.Longitude);
        area.Status = request.Status;
        area.MapPath = Clean(request.MapPath);
    }

    public static BuildingEntity ToEntity(this CreateBuildingRequest request)
    {
        return new BuildingEntity
        {
            ProjectId = Clean(request.ProjectId),
            CountryId = Clean(request.CountryId),
            AreaId = Clean(request.AreaId),
            BuildingName = Clean(request.BuildingName),
            Description = Clean(request.Description),
            Latitude = Clean(request.Latitude),
            Longitude = Clean(request.Longitude),
            Status = request.Status,
            CreatedBy = Clean(request.CreatedBy),
            CreatedAt = DateTime.UtcNow,
            ClientId = Clean(request.ClientId)
        };
    }

    public static void ApplyTo(this UpdateBuildingRequest request, BuildingEntity building)
    {
        building.BuildingName = Clean(request.BuildingName);
        building.Description = Clean(request.Description);
        building.Latitude = Clean(request.Latitude);
        building.Longitude = Clean(request.Longitude);
        building.Status = request.Status;
    }

    public static FloorEntity ToEntity(this CreateFloorRequest request)
    {
        return new FloorEntity
        {
            ProjectId = Clean(request.ProjectId),
            CountryId = Clean(request.CountryId),
            AreaId = Clean(request.AreaId),
            BuildingId = Clean(request.BuildingId),
            FloorName = Clean(request.FloorName),
            Description = Clean(request.Description),
            Status = request.Status,
            CreatedBy = Clean(request.CreatedBy),
            CreatedAt = DateTime.UtcNow,
            ClientId = Clean(request.ClientId),
            MapPath = Clean(request.MapPath)
        };
    }

    public static void ApplyTo(this UpdateFloorRequest request, FloorEntity floor)
    {
        floor.FloorName = Clean(request.FloorName);
        floor.Description = Clean(request.Description);
        floor.Status = request.Status;
        floor.MapPath = Clean(request.MapPath);
    }

    public static ZoneEntity ToEntity(this CreateZoneRequest request)
    {
        return new ZoneEntity
        {
            ProjectId = Clean(request.ProjectId),
            CountryId = Clean(request.CountryId),
            AreaId = Clean(request.AreaId),
            BuildingId = Clean(request.BuildingId),
            FloorId = Clean(request.FloorId),
            ZoneName = Clean(request.ZoneName),
            Description = Clean(request.Description),
            TopZone = Clean(request.TopZone),
            Priority = Clean(request.Priority),
            MusterPoint = request.MusterPoint,
            ExitPoint = request.ExitPoint,
            Status = request.Status,
            CreatedBy = Clean(request.CreatedBy),
            CreatedAt = DateTime.UtcNow,
            ClientId = Clean(request.ClientId),
            TimeTakenAssemblePoint = request.TimeTakenAssemblePoint,
            MapPath = Clean(request.MapPath)
        };
    }

    public static void ApplyTo(this UpdateZoneRequest request, ZoneEntity zone)
    {
        zone.ZoneName = Clean(request.ZoneName);
        zone.Description = Clean(request.Description);
        zone.TopZone = Clean(request.TopZone);
        zone.Priority = Clean(request.Priority);
        zone.MusterPoint = request.MusterPoint;
        zone.ExitPoint = request.ExitPoint;
        zone.Status = request.Status;
        zone.TimeTakenAssemblePoint = request.TimeTakenAssemblePoint;
        zone.MapPath = Clean(request.MapPath);
    }

    public static SubZoneEntity ToEntity(this CreateSubZoneRequest request)
    {
        return new SubZoneEntity
        {
            ProjectId = Clean(request.ProjectId),
            CountryId = Clean(request.CountryId),
            AreaId = Clean(request.AreaId),
            BuildingId = Clean(request.BuildingId),
            FloorId = Clean(request.FloorId),
            ZoneId = Clean(request.ZoneId),
            SubZoneName = Clean(request.SubZoneName),
            Description = Clean(request.Description),
            TopZone = request.TopZone.ToString(),
            Priority = request.Priority.ToString(),
            AssemblyPoint = request.AssemblyPoint,
            Exit = request.Exit ? "1" : "0",
            Status = request.Status,
            CreatedBy = Clean(request.CreatedBy),
            CreatedAt = DateTime.UtcNow,
            ClientId = Clean(request.ClientId),
            TimeTakenAssemblePoint = request.TimeTakenAssemblePoint?.ToString() ?? string.Empty,
            MapPath = Clean(request.MapPath)
        };
    }

    public static void ApplyTo(this UpdateSubZoneRequest request, SubZoneEntity subZone)
    {
        subZone.SubZoneName = Clean(request.SubZoneName);
        subZone.Description = Clean(request.Description);
        subZone.TopZone = request.TopZone.ToString();
        subZone.Priority = request.Priority.ToString();
        subZone.AssemblyPoint = request.AssemblyPoint;
        subZone.Exit = request.Exit ? "1" : "0";
        subZone.Status = request.Status;
        subZone.TimeTakenAssemblePoint = request.TimeTakenAssemblePoint?.ToString() ?? string.Empty;
        subZone.MapPath = Clean(request.MapPath);
    }

    public static ZoneMappingEntity ToEntity(this CreateZoneMappingRequest request)
    {
        return new ZoneMappingEntity
        {
            ProjectId = Clean(request.ProjectId),
            CountryId = Clean(request.CountryId),
            AreaId = Clean(request.AreaId),
            BuildingId = Clean(request.BuildingId),
            FloorId = Clean(request.FloorId),
            ZoneId = Clean(request.ZoneId),
            ZoneName = Clean(request.ZoneName),
            Description = request.Description,
            TopZone = request.TopZone,
            Priority = request.Priority,
            AssemblyPoint = request.AssemblyPoint,
            Exit = request.Exit,
            Status = request.Status,
            CreatedBy = Clean(request.CreatedBy),
            CreatedAt = DateTime.UtcNow,
            ClientId = Clean(request.ClientId),
            ZoneColour = request.ZoneColour,
            GeoJsonData = GeoJsonConversion.ToBsonDocuments(request.GeoJsonData)
        };
    }

    public static void ApplyTo(this UpdateZoneMappingRequest request, ZoneMappingEntity zoneMapping)
    {
        zoneMapping.ZoneName = Clean(request.ZoneName);
        zoneMapping.Description = request.Description;
        zoneMapping.TopZone = request.TopZone;
        zoneMapping.Priority = request.Priority;
        zoneMapping.AssemblyPoint = request.AssemblyPoint;
        zoneMapping.Exit = request.Exit;
        zoneMapping.Status = request.Status;
        zoneMapping.ZoneColour = request.ZoneColour;
        zoneMapping.GeoJsonData = GeoJsonConversion.ToBsonDocuments(request.GeoJsonData);
    }

    public static DeviceZoneMappingEntity ToEntity(this CreateDeviceZoneMappingRequest request)
    {
        return new DeviceZoneMappingEntity
        {
            ProjectId = Clean(request.ProjectId),
            CountryId = Clean(request.CountryId),
            AreaId = Clean(request.AreaId),
            BuildingId = Clean(request.BuildingId),
            FloorId = Clean(request.FloorId),
            ZoneId = Clean(request.ZoneId),
            ZoneName = Clean(request.ZoneName),
            DeviceReferenceId = Clean(request.DeviceReferenceId),
            DeviceName = Clean(request.DeviceName),
            Description = request.Description,
            TopZone = request.TopZone,
            Priority = request.Priority,
            AssemblyPoint = request.AssemblyPoint,
            Exit = request.Exit,
            Status = request.Status,
            CreatedBy = Clean(request.CreatedBy),
            CreatedAt = DateTime.UtcNow,
            ClientId = Clean(request.ClientId),
            DeviceGeoJsonData = GeoJsonConversion.ToBsonDocuments(request.DeviceGeoJsonData)
        };
    }

    public static void ApplyTo(this UpdateDeviceZoneMappingRequest request, DeviceZoneMappingEntity deviceZoneMapping)
    {
        deviceZoneMapping.DeviceName = Clean(request.DeviceName);
        deviceZoneMapping.Description = request.Description;
        deviceZoneMapping.TopZone = request.TopZone;
        deviceZoneMapping.Priority = request.Priority;
        deviceZoneMapping.AssemblyPoint = request.AssemblyPoint;
        deviceZoneMapping.Exit = request.Exit;
        deviceZoneMapping.Status = request.Status;
        deviceZoneMapping.DeviceGeoJsonData = GeoJsonConversion.ToBsonDocuments(request.DeviceGeoJsonData);
    }

    private static string Clean(string? value)
    {
        return value?.Trim() ?? string.Empty;
    }
}
