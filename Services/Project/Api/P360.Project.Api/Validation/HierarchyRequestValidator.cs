using P360.Project.Api.Contracts;

namespace P360.Project.Api.Validation;

internal static class HierarchyRequestValidator
{
    public static IDictionary<string, string[]> Validate(this CreateCountryRequest request)
    {
        var errors = new ValidationErrorBuilder();

        errors.ObjectId(nameof(request.ProjectId), request.ProjectId, "Project id");
        errors.Required(nameof(request.CountryName), request.CountryName, "Country name");
        errors.Required(nameof(request.CreatedBy), request.CreatedBy, "Created by");
        errors.Required(nameof(request.ClientId), request.ClientId, "Client id");

        return errors.ToDictionary();
    }

    public static IDictionary<string, string[]> Validate(this UpdateCountryRequest request)
    {
        var errors = new ValidationErrorBuilder();

        errors.Required(nameof(request.CountryName), request.CountryName, "Country name");
        errors.Required(nameof(request.ClientId), request.ClientId, "Client id");

        return errors.ToDictionary();
    }

    public static IDictionary<string, string[]> Validate(this CreateAreaRequest request)
    {
        var errors = new ValidationErrorBuilder();

        errors.ObjectId(nameof(request.ProjectId), request.ProjectId, "Project id");
        errors.ObjectId(nameof(request.CountryId), request.CountryId, "Country id");
        errors.Required(nameof(request.AreaName), request.AreaName, "Area name");
        errors.Required(nameof(request.CreatedBy), request.CreatedBy, "Created by");
        errors.Required(nameof(request.ClientId), request.ClientId, "Client id");

        return errors.ToDictionary();
    }

    public static IDictionary<string, string[]> Validate(this UpdateAreaRequest request)
    {
        var errors = new ValidationErrorBuilder();

        errors.Required(nameof(request.AreaName), request.AreaName, "Area name");

        return errors.ToDictionary();
    }

    public static IDictionary<string, string[]> Validate(this CreateBuildingRequest request)
    {
        var errors = new ValidationErrorBuilder();

        errors.ObjectId(nameof(request.ProjectId), request.ProjectId, "Project id");
        errors.ObjectId(nameof(request.CountryId), request.CountryId, "Country id");
        errors.ObjectId(nameof(request.AreaId), request.AreaId, "Area id");
        errors.Required(nameof(request.BuildingName), request.BuildingName, "Building name");
        errors.Required(nameof(request.CreatedBy), request.CreatedBy, "Created by");
        errors.Required(nameof(request.ClientId), request.ClientId, "Client id");

        return errors.ToDictionary();
    }

    public static IDictionary<string, string[]> Validate(this UpdateBuildingRequest request)
    {
        var errors = new ValidationErrorBuilder();

        errors.Required(nameof(request.BuildingName), request.BuildingName, "Building name");

        return errors.ToDictionary();
    }

    public static IDictionary<string, string[]> Validate(this CreateFloorRequest request)
    {
        var errors = new ValidationErrorBuilder();

        errors.ObjectId(nameof(request.ProjectId), request.ProjectId, "Project id");
        errors.ObjectId(nameof(request.CountryId), request.CountryId, "Country id");
        errors.ObjectId(nameof(request.AreaId), request.AreaId, "Area id");
        errors.ObjectId(nameof(request.BuildingId), request.BuildingId, "Building id");
        errors.Required(nameof(request.FloorName), request.FloorName, "Floor name");
        errors.Required(nameof(request.CreatedBy), request.CreatedBy, "Created by");
        errors.Required(nameof(request.ClientId), request.ClientId, "Client id");

        return errors.ToDictionary();
    }

    public static IDictionary<string, string[]> Validate(this UpdateFloorRequest request)
    {
        var errors = new ValidationErrorBuilder();

        errors.Required(nameof(request.FloorName), request.FloorName, "Floor name");

        return errors.ToDictionary();
    }

    public static IDictionary<string, string[]> Validate(this CreateZoneRequest request)
    {
        var errors = new ValidationErrorBuilder();

        errors.ObjectId(nameof(request.ProjectId), request.ProjectId, "Project id");
        errors.ObjectId(nameof(request.CountryId), request.CountryId, "Country id");
        errors.ObjectId(nameof(request.AreaId), request.AreaId, "Area id");
        errors.ObjectId(nameof(request.BuildingId), request.BuildingId, "Building id");
        errors.ObjectId(nameof(request.FloorId), request.FloorId, "Floor id");
        errors.Required(nameof(request.ZoneName), request.ZoneName, "Zone name");
        errors.Required(nameof(request.CreatedBy), request.CreatedBy, "Created by");
        errors.Required(nameof(request.ClientId), request.ClientId, "Client id");
        errors.NonNegative(nameof(request.TimeTakenAssemblePoint), request.TimeTakenAssemblePoint, "Time taken to assemble point");

        return errors.ToDictionary();
    }

    public static IDictionary<string, string[]> Validate(this UpdateZoneRequest request)
    {
        var errors = new ValidationErrorBuilder();

        errors.Required(nameof(request.ZoneName), request.ZoneName, "Zone name");
        errors.NonNegative(nameof(request.TimeTakenAssemblePoint), request.TimeTakenAssemblePoint, "Time taken to assemble point");

        return errors.ToDictionary();
    }

    public static IDictionary<string, string[]> Validate(this CreateSubZoneRequest request)
    {
        var errors = new ValidationErrorBuilder();

        errors.ObjectId(nameof(request.ProjectId), request.ProjectId, "Project id");
        errors.ObjectId(nameof(request.CountryId), request.CountryId, "Country id");
        errors.ObjectId(nameof(request.AreaId), request.AreaId, "Area id");
        errors.ObjectId(nameof(request.BuildingId), request.BuildingId, "Building id");
        errors.ObjectId(nameof(request.FloorId), request.FloorId, "Floor id");
        errors.ObjectId(nameof(request.ZoneId), request.ZoneId, "Zone id");
        errors.Required(nameof(request.SubZoneName), request.SubZoneName, "Sub zone name");
        errors.Required(nameof(request.CreatedBy), request.CreatedBy, "Created by");
        errors.Required(nameof(request.ClientId), request.ClientId, "Client id");
        errors.NonNegative(nameof(request.Priority), request.Priority, "Priority");
        errors.NonNegative(nameof(request.TimeTakenAssemblePoint), request.TimeTakenAssemblePoint, "Time taken to assemble point");

        return errors.ToDictionary();
    }

    public static IDictionary<string, string[]> Validate(this UpdateSubZoneRequest request)
    {
        var errors = new ValidationErrorBuilder();

        errors.Required(nameof(request.SubZoneName), request.SubZoneName, "Sub zone name");
        errors.NonNegative(nameof(request.Priority), request.Priority, "Priority");
        errors.NonNegative(nameof(request.TimeTakenAssemblePoint), request.TimeTakenAssemblePoint, "Time taken to assemble point");

        return errors.ToDictionary();
    }

    public static IDictionary<string, string[]> Validate(this CreateZoneMappingRequest request)
    {
        var errors = new ValidationErrorBuilder();

        errors.ObjectId(nameof(request.ProjectId), request.ProjectId, "Project id");
        errors.ObjectId(nameof(request.CountryId), request.CountryId, "Country id");
        errors.ObjectId(nameof(request.AreaId), request.AreaId, "Area id");
        errors.ObjectId(nameof(request.BuildingId), request.BuildingId, "Building id");
        errors.ObjectId(nameof(request.FloorId), request.FloorId, "Floor id");
        errors.ObjectId(nameof(request.ZoneId), request.ZoneId, "Zone id");
        errors.Required(nameof(request.ZoneName), request.ZoneName, "Zone name");
        errors.Required(nameof(request.CreatedBy), request.CreatedBy, "Created by");
        errors.Required(nameof(request.ClientId), request.ClientId, "Client id");

        return errors.ToDictionary();
    }

    public static IDictionary<string, string[]> Validate(this UpdateZoneMappingRequest request)
    {
        var errors = new ValidationErrorBuilder();

        errors.Required(nameof(request.ZoneName), request.ZoneName, "Zone name");

        return errors.ToDictionary();
    }

    public static IDictionary<string, string[]> Validate(this CreateDeviceZoneMappingRequest request)
    {
        var errors = new ValidationErrorBuilder();

        errors.ObjectId(nameof(request.ProjectId), request.ProjectId, "Project id");
        errors.ObjectId(nameof(request.CountryId), request.CountryId, "Country id");
        errors.ObjectId(nameof(request.AreaId), request.AreaId, "Area id");
        errors.ObjectId(nameof(request.BuildingId), request.BuildingId, "Building id");
        errors.ObjectId(nameof(request.FloorId), request.FloorId, "Floor id");
        errors.ObjectId(nameof(request.ZoneId), request.ZoneId, "Zone id");
        errors.Required(nameof(request.ZoneName), request.ZoneName, "Zone name");
        errors.ObjectId(nameof(request.DeviceReferenceId), request.DeviceReferenceId, "Device reference id");
        errors.Required(nameof(request.DeviceName), request.DeviceName, "Device name");
        errors.Required(nameof(request.CreatedBy), request.CreatedBy, "Created by");
        errors.Required(nameof(request.ClientId), request.ClientId, "Client id");

        return errors.ToDictionary();
    }

    public static IDictionary<string, string[]> Validate(this UpdateDeviceZoneMappingRequest request)
    {
        var errors = new ValidationErrorBuilder();

        errors.Required(nameof(request.DeviceName), request.DeviceName, "Device name");

        return errors.ToDictionary();
    }
}
