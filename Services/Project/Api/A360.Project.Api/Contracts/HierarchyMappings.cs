using AreaEntity = A360.Project.Domain.Entities.Area;
using BuildingEntity = A360.Project.Domain.Entities.Building;
using CountryEntity = A360.Project.Domain.Entities.Country;
using DeviceZoneMappingEntity = A360.Project.Domain.Entities.DeviceZoneMapping;
using FloorEntity = A360.Project.Domain.Entities.Floor;
using OuterZoneEntity = A360.Project.Domain.Entities.OuterZone;
using SubZoneEntity = A360.Project.Domain.Entities.SubZone;
using ZoneEntity = A360.Project.Domain.Entities.Zone;
using ZoneMappingEntity = A360.Project.Domain.Entities.ZoneMapping;

namespace A360.Project.Api.Contracts;

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
            ClientId = Clean(request.ClientId),
            TenantId = request.TenantId,
            IsDeleted = false
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
        country.UpdatedBy = request.UpdatedBy;
        country.UpdatedAt = DateTime.UtcNow;
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
            TenantId = request.TenantId,
            IsDeleted = false,
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
        area.UpdatedBy = request.UpdatedBy;
        area.UpdatedAt = DateTime.UtcNow;
    }

    public static OuterZoneEntity ToEntity(this CreateOuterZoneRequest request)
    {
        return new OuterZoneEntity
        {
            ProjectId = Clean(request.ProjectId),
            CountryId = Clean(request.CountryId),
            AreaId = Clean(request.AreaId),
            OuterZoneName = Clean(request.OuterZoneName),
            Description = Clean(request.Description),
            OutlineMap = Clean(request.OutlineMap),
            Latitude = Clean(request.Latitude),
            Longitude = Clean(request.Longitude),
            Status = request.Status,
            CreatedBy = Clean(request.CreatedBy),
            CreatedAt = DateTime.UtcNow,
            ClientId = Clean(request.ClientId),
            TenantId = request.TenantId,
            IsDeleted = false,
            MapPath = Clean(request.MapPath)
        };
    }

    public static void ApplyTo(this UpdateOuterZoneRequest request, OuterZoneEntity outerZone)
    {
        outerZone.OuterZoneName = Clean(request.OuterZoneName);
        outerZone.Description = Clean(request.Description);
        outerZone.OutlineMap = Clean(request.OutlineMap);
        outerZone.Latitude = Clean(request.Latitude);
        outerZone.Longitude = Clean(request.Longitude);
        outerZone.Status = request.Status;
        outerZone.MapPath = Clean(request.MapPath);
        outerZone.UpdatedBy = request.UpdatedBy;
        outerZone.UpdatedAt = DateTime.UtcNow;
    }

    public static BuildingEntity ToEntity(this CreateBuildingRequest request)
    {
        return new BuildingEntity
        {
            ProjectId = Clean(request.ProjectId),
            CountryId = Clean(request.CountryId),
            AreaId = Clean(request.AreaId),
            OuterZoneId = Clean(request.OuterZoneId),
            BuildingName = Clean(request.BuildingName),
            Description = Clean(request.Description),
            Latitude = Clean(request.Latitude),
            Longitude = Clean(request.Longitude),
            Status = request.Status,
            CreatedBy = Clean(request.CreatedBy),
            CreatedAt = DateTime.UtcNow,
            ClientId = Clean(request.ClientId),
            TenantId = request.TenantId,
            IsDeleted = false
        };
    }

    public static void ApplyTo(this UpdateBuildingRequest request, BuildingEntity building)
    {
        building.BuildingName = Clean(request.BuildingName);
        building.Description = Clean(request.Description);
        building.Latitude = Clean(request.Latitude);
        building.Longitude = Clean(request.Longitude);
        building.Status = request.Status;
        building.UpdatedBy = request.UpdatedBy;
        building.UpdatedAt = DateTime.UtcNow;
    }

    public static FloorEntity ToEntity(this CreateFloorRequest request)
    {
        return new FloorEntity
        {
            ProjectId = Clean(request.ProjectId),
            CountryId = Clean(request.CountryId),
            AreaId = Clean(request.AreaId),
            OuterZoneId = Clean(request.OuterZoneId),
            BuildingId = Clean(request.BuildingId),
            FloorName = Clean(request.FloorName),
            Description = Clean(request.Description),
            Status = request.Status,
            CreatedBy = Clean(request.CreatedBy),
            CreatedAt = DateTime.UtcNow,
            ClientId = Clean(request.ClientId),
            TenantId = request.TenantId,
            IsDeleted = false,
            MapPath = Clean(request.MapPath)
        };
    }

    public static void ApplyTo(this UpdateFloorRequest request, FloorEntity floor)
    {
        floor.FloorName = Clean(request.FloorName);
        floor.Description = Clean(request.Description);
        floor.Status = request.Status;
        floor.MapPath = Clean(request.MapPath);
        floor.UpdatedBy = request.UpdatedBy;
        floor.UpdatedAt = DateTime.UtcNow;
    }

    public static ZoneEntity ToEntity(this CreateZoneRequest request)
    {
        return new ZoneEntity
        {
            ProjectId = Clean(request.ProjectId),
            CountryId = Clean(request.CountryId),
            AreaId = Clean(request.AreaId),
            OuterZoneId = Clean(request.OuterZoneId),
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
            TenantId = request.TenantId,
            IsDeleted = false,
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
        zone.UpdatedBy = request.UpdatedBy;
        zone.UpdatedAt = DateTime.UtcNow;
    }

    public static SubZoneEntity ToEntity(this CreateSubZoneRequest request)
    {
        return new SubZoneEntity
        {
            ProjectId = Clean(request.ProjectId),
            CountryId = Clean(request.CountryId),
            AreaId = Clean(request.AreaId),
            OuterZoneId = Clean(request.OuterZoneId),
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
            TenantId = request.TenantId,
            IsDeleted = false,
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
        subZone.UpdatedBy = request.UpdatedBy;
        subZone.UpdatedAt = DateTime.UtcNow;
    }

    public static ZoneMappingEntity ToEntity(this CreateZoneMappingRequest request)
    {
        return new ZoneMappingEntity
        {
            ProjectId = Clean(request.ProjectId),
            CountryId = Clean(request.CountryId),
            AreaId = Clean(request.AreaId),
            OuterZoneId = Clean(request.OuterZoneId),
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
            TenantId = request.TenantId,
            IsDeleted = false,
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
        zoneMapping.UpdatedBy = request.UpdatedBy;
        zoneMapping.UpdatedAt = DateTime.UtcNow;
    }

    public static DeviceZoneMappingEntity ToEntity(this CreateDeviceZoneMappingRequest request)
    {
        return new DeviceZoneMappingEntity
        {
            ProjectId = Clean(request.ProjectId),
            CountryId = Clean(request.CountryId),
            AreaId = Clean(request.AreaId),
            OuterZoneId = Clean(request.OuterZoneId),
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
            TenantId = request.TenantId,
            IsDeleted = false,
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
        deviceZoneMapping.UpdatedBy = request.UpdatedBy;
        deviceZoneMapping.UpdatedAt = DateTime.UtcNow;
    }

    private static string Clean(string? value)
    {
        return value?.Trim() ?? string.Empty;
    }
}
