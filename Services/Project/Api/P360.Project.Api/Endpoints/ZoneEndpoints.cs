using P360.Project.Api.Contracts;
using P360.Project.Api.Services;
using P360.Project.Api.Validation;
using P360.Project.Repository.Repositories;
using P360.Repository.Repositories;

namespace P360.Project.Api.Endpoints;

public static class ZoneEndpoints
{
    private const string MapCategory = "zonemaps";

    public static RouteGroupBuilder MapZoneEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/zones").WithTags("Zones");

        group.MapGet("", GetZonesAsync).WithName("GetZones");
        group.MapGet("/{id}", GetZoneByIdAsync).WithName("GetZoneById");
        group.MapPost("", CreateZoneAsync).WithName("CreateZone");
        group.MapPut("/{id}", UpdateZoneAsync).WithName("UpdateZone");
        group.MapDelete("/{id}", DeleteZoneAsync).WithName("DeleteZone");
        group.MapPost("/{id}/map", UploadZoneMapAsync).WithName("UploadZoneMap").DisableAntiforgery();
        group.MapGet("/{id}/map", GetZoneMapAsync).WithName("GetZoneMap");

        return group;
    }

    private static async Task<IResult> GetZonesAsync(
        IZoneRepository repository,
        CancellationToken cancellationToken)
    {
        var zones = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(zones.Select(ZoneResponse.FromEntity));
    }

    private static async Task<IResult> GetZoneByIdAsync(
        string id,
        IZoneRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid zone id." });
        }

        var zone = await repository.GetByIdAsync(id, cancellationToken);
        return zone is null ? Results.NotFound() : Results.Ok(ZoneResponse.FromEntity(zone));
    }

    private static async Task<IResult> CreateZoneAsync(
        CreateZoneRequest request,
        IZoneRepository repository,
        IProjectRepository projectRepository,
        ICountryRepository countryRepository,
        IAreaRepository areaRepository,
        IBuildingRepository buildingRepository,
        IFloorRepository floorRepository,
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
            cancellationToken);
        if (relationshipErrors.Count > 0)
        {
            return Results.ValidationProblem(relationshipErrors);
        }

        var zone = await repository.CreateAsync(request.ToEntity(), cancellationToken);
        return Results.Created($"/api/zones/{zone.Id}", ZoneResponse.FromEntity(zone));
    }

    private static async Task<IResult> UpdateZoneAsync(
        string id,
        UpdateZoneRequest request,
        IZoneRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid zone id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var zone = await repository.GetByIdAsync(id, cancellationToken);
        if (zone is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(zone);

        var updated = await repository.UpdateAsync(id, zone, cancellationToken);
        return updated ? Results.Ok(ZoneResponse.FromEntity(zone)) : Results.NotFound();
    }

    private static async Task<IResult> DeleteZoneAsync(
        string id,
        IZoneRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid zone id." });
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UploadZoneMapAsync(
        string id,
        IFormFile file,
        IZoneRepository repository,
        IMapFileStorageService mapFileStorageService,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid zone id." });
        }

        if (file.Length == 0)
        {
            return Results.BadRequest(new { message = "Map file is required." });
        }

        var zone = await repository.GetByIdAsync(id, cancellationToken);
        if (zone is null)
        {
            return Results.NotFound();
        }

        string mapPath;
        try
        {
            mapPath = await mapFileStorageService.SaveMapFileAsync(file, MapCategory, cancellationToken);
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { message = ex.Message });
        }

        zone.MapPath = mapPath;

        var updated = await repository.UpdateAsync(id, zone, cancellationToken);
        return updated ? Results.Ok(ZoneResponse.FromEntity(zone)) : Results.NotFound();
    }

    private static async Task<IResult> GetZoneMapAsync(
        string id,
        IZoneRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid zone id." });
        }

        var zone = await repository.GetByIdAsync(id, cancellationToken);
        if (zone is null || string.IsNullOrEmpty(zone.MapPath))
        {
            return Results.NotFound();
        }

        return Results.Ok(new ZoneMapResponse(zone.Id, zone.MapPath));
    }
}
