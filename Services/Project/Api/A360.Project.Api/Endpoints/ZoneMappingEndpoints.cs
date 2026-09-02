using A360.Project.Api.Contracts;
using A360.Project.Api.Validation;
using A360.Project.Repository.Repositories;
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
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid zone mapping id." });
        }

        var zoneMapping = await repository.GetByIdAsync(id, cancellationToken);
        return zoneMapping is null ? Results.NotFound() : Results.Ok(ZoneMappingResponse.FromEntity(zoneMapping));
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
        return Results.Created($"/api/zone-mappings/{zoneMapping.Id}", ZoneMappingResponse.FromEntity(zoneMapping));
    }

    private static async Task<IResult> UpdateZoneMappingAsync(
        string id,
        UpdateZoneMappingRequest request,
        IZoneMappingRepository repository,
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
        return updated ? Results.Ok(ZoneMappingResponse.FromEntity(zoneMapping)) : Results.NotFound();
    }

    private static async Task<IResult> DeleteZoneMappingAsync(
        string id,
        IZoneMappingRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid zone mapping id." });
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
