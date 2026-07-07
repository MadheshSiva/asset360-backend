using A360.Project.Api.Contracts;
using A360.Project.Api.Validation;
using A360.Project.Repository.Repositories;
using A360.Repository.Repositories;

namespace A360.Project.Api.Endpoints;

public static class DeviceZoneMappingEndpoints
{
    public static RouteGroupBuilder MapDeviceZoneMappingEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/device-zone-mappings").WithTags("Device Zone Mappings");

        group.MapGet("", GetDeviceZoneMappingsAsync).WithName("GetDeviceZoneMappings");
        group.MapGet("/{id}", GetDeviceZoneMappingByIdAsync).WithName("GetDeviceZoneMappingById");
        group.MapPost("", CreateDeviceZoneMappingAsync).WithName("CreateDeviceZoneMapping");
        group.MapPut("/{id}", UpdateDeviceZoneMappingAsync).WithName("UpdateDeviceZoneMapping");
        group.MapDelete("/{id}", DeleteDeviceZoneMappingAsync).WithName("DeleteDeviceZoneMapping");

        return group;
    }

    private static async Task<IResult> GetDeviceZoneMappingsAsync(
        IDeviceZoneMappingRepository repository,
        CancellationToken cancellationToken)
    {
        var deviceZoneMappings = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(deviceZoneMappings.Select(DeviceZoneMappingResponse.FromEntity));
    }

    private static async Task<IResult> GetDeviceZoneMappingByIdAsync(
        string id,
        IDeviceZoneMappingRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid device zone mapping id." });
        }

        var deviceZoneMapping = await repository.GetByIdAsync(id, cancellationToken);
        return deviceZoneMapping is null ? Results.NotFound() : Results.Ok(DeviceZoneMappingResponse.FromEntity(deviceZoneMapping));
    }

    private static async Task<IResult> CreateDeviceZoneMappingAsync(
        CreateDeviceZoneMappingRequest request,
        IDeviceZoneMappingRepository repository,
        IProjectRepository projectRepository,
        ICountryRepository countryRepository,
        IAreaRepository areaRepository,
        IBuildingRepository buildingRepository,
        IFloorRepository floorRepository,
        IZoneRepository zoneRepository,
        CancellationToken cancellationToken)
    {
        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var relationshipErrors = await request.ValidateRelationshipsAsync(
            projectRepository,
            countryRepository,
            areaRepository,
            buildingRepository,
            floorRepository,
            zoneRepository,
            cancellationToken);
        if (relationshipErrors.Count > 0)
        {
            return Results.ValidationProblem(relationshipErrors);
        }

        var deviceZoneMapping = await repository.CreateAsync(request.ToEntity(), cancellationToken);
        return Results.Created($"/api/device-zone-mappings/{deviceZoneMapping.Id}", DeviceZoneMappingResponse.FromEntity(deviceZoneMapping));
    }

    private static async Task<IResult> UpdateDeviceZoneMappingAsync(
        string id,
        UpdateDeviceZoneMappingRequest request,
        IDeviceZoneMappingRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid device zone mapping id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var deviceZoneMapping = await repository.GetByIdAsync(id, cancellationToken);
        if (deviceZoneMapping is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(deviceZoneMapping);

        var updated = await repository.UpdateAsync(id, deviceZoneMapping, cancellationToken);
        return updated ? Results.Ok(DeviceZoneMappingResponse.FromEntity(deviceZoneMapping)) : Results.NotFound();
    }

    private static async Task<IResult> DeleteDeviceZoneMappingAsync(
        string id,
        IDeviceZoneMappingRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid device zone mapping id." });
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
