using A360.Domain.Entities;
using A360.Project.Api.Contracts;
using A360.Project.Api.Validation;
using A360.Project.Repository.Repositories;
using A360.Repository.Activity;
using A360.Repository.Repositories;

namespace A360.Project.Api.Endpoints;

public static class ZoneMappingEndpoints
{
    public static RouteGroupBuilder MapZoneMappingEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/zone-mappings").WithTags("Zone Mappings");

        group.MapGet("", GetZoneMappingsAsync).WithName("GetZoneMappings");
        group.MapGet("/{id}", GetZoneMappingByIdAsync).WithName("GetZoneMappingById");
        group.MapPost("", CreateZoneMappingAsync).WithName("CreateZoneMapping");
        group.MapPut("/{id}", UpdateZoneMappingAsync).WithName("UpdateZoneMapping");
        group.MapDelete("/{id}", DeleteZoneMappingAsync).WithName("DeleteZoneMapping");

        return group;
    }

    private static async Task<IResult> GetZoneMappingsAsync(
        IZoneMappingRepository repository,
        CancellationToken cancellationToken)
    {
        var zoneMappings = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(zoneMappings.Select(ZoneMappingResponse.FromEntity));
    }

    private static async Task<IResult> GetZoneMappingByIdAsync(
        string id,
        IZoneMappingRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid zone mapping id." });
        }

        var zoneMapping = await repository.GetByIdAsync(id, cancellationToken);
        if (zoneMapping is null)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("ZoneMapping", zoneMapping.Id, zoneMapping.ZoneName, EventAction.Viewed, cancellationToken);

        return Results.Ok(ZoneMappingResponse.FromEntity(zoneMapping));
    }

    private static async Task<IResult> CreateZoneMappingAsync(
        CreateZoneMappingRequest request,
        IZoneMappingRepository repository,
        IProjectRepository projectRepository,
        ICountryRepository countryRepository,
        IAreaRepository areaRepository,
        IOuterZoneRepository outerZoneRepository,
        IBuildingRepository buildingRepository,
        IFloorRepository floorRepository,
        IZoneRepository zoneRepository,
        IEventLogger eventLogger,
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
            outerZoneRepository,
            buildingRepository,
            floorRepository,
            zoneRepository,
            cancellationToken);
        if (relationshipErrors.Count > 0)
        {
            return Results.ValidationProblem(relationshipErrors);
        }

        var zoneMapping = await repository.CreateAsync(request.ToEntity(), cancellationToken);

        await eventLogger.LogAsync("ZoneMapping", zoneMapping.Id, zoneMapping.ZoneName, EventAction.Created, cancellationToken);

        return Results.Created($"/api/zone-mappings/{zoneMapping.Id}", ZoneMappingResponse.FromEntity(zoneMapping));
    }

    private static async Task<IResult> UpdateZoneMappingAsync(
        string id,
        UpdateZoneMappingRequest request,
        IZoneMappingRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid zone mapping id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var zoneMapping = await repository.GetByIdAsync(id, cancellationToken);
        if (zoneMapping is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(zoneMapping);

        var updated = await repository.UpdateAsync(id, zoneMapping, cancellationToken);
        if (!updated)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("ZoneMapping", zoneMapping.Id, zoneMapping.ZoneName, EventAction.Updated, cancellationToken);

        return Results.Ok(ZoneMappingResponse.FromEntity(zoneMapping));
    }

    private static async Task<IResult> DeleteZoneMappingAsync(
        string id,
        IZoneMappingRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid zone mapping id." });
        }

        var zoneMapping = await repository.GetByIdAsync(id, cancellationToken);
        if (zoneMapping is null)
        {
            return Results.NotFound();
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        if (!deleted)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("ZoneMapping", zoneMapping.Id, zoneMapping.ZoneName, EventAction.Deleted, cancellationToken);

        return Results.NoContent();
    }
}
