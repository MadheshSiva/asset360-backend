using A360.Domain.Entities;
using A360.Project.Api.Contracts;
using A360.Project.Api.Services;
using A360.Project.Api.Validation;
using A360.Project.Repository.Repositories;
using A360.Repository.Activity;
using A360.Repository.Repositories;

namespace A360.Project.Api.Endpoints;

public static class SubZoneEndpoints
{
    private const string MapCategory = "subzonemaps";

    public static RouteGroupBuilder MapSubZoneEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/sub-zones").WithTags("Sub Zones");

        group.MapGet("", GetSubZonesAsync).WithName("GetSubZones");
        group.MapGet("/{id}", GetSubZoneByIdAsync).WithName("GetSubZoneById");
        group.MapPost("", CreateSubZoneAsync).WithName("CreateSubZone");
        group.MapPut("/{id}", UpdateSubZoneAsync).WithName("UpdateSubZone");
        group.MapDelete("/{id}", DeleteSubZoneAsync).WithName("DeleteSubZone");
        group.MapPost("/{id}/map", UploadSubZoneMapAsync).WithName("UploadSubZoneMap").DisableAntiforgery();
        group.MapGet("/{id}/map", GetSubZoneMapAsync).WithName("GetSubZoneMap");

        return group;
    }

    private static async Task<IResult> GetSubZonesAsync(
        ISubZoneRepository repository,
        CancellationToken cancellationToken)
    {
        var subZones = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(subZones.Select(SubZoneResponse.FromEntity));
    }

    private static async Task<IResult> GetSubZoneByIdAsync(
        string id,
        ISubZoneRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid sub zone id." });
        }

        var subZone = await repository.GetByIdAsync(id, cancellationToken);
        if (subZone is null)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("SubZone", subZone.Id, subZone.SubZoneName, EventAction.Viewed, cancellationToken);

        return Results.Ok(SubZoneResponse.FromEntity(subZone));
    }

    private static async Task<IResult> CreateSubZoneAsync(
        CreateSubZoneRequest request,
        ISubZoneRepository repository,
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

        var subZone = await repository.CreateAsync(request.ToEntity(), cancellationToken);

        await eventLogger.LogAsync("SubZone", subZone.Id, subZone.SubZoneName, EventAction.Created, cancellationToken);

        return Results.Created($"/api/sub-zones/{subZone.Id}", SubZoneResponse.FromEntity(subZone));
    }

    private static async Task<IResult> UpdateSubZoneAsync(
        string id,
        UpdateSubZoneRequest request,
        ISubZoneRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid sub zone id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var subZone = await repository.GetByIdAsync(id, cancellationToken);
        if (subZone is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(subZone);

        var updated = await repository.UpdateAsync(id, subZone, cancellationToken);
        if (!updated)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("SubZone", subZone.Id, subZone.SubZoneName, EventAction.Updated, cancellationToken);

        return Results.Ok(SubZoneResponse.FromEntity(subZone));
    }

    private static async Task<IResult> DeleteSubZoneAsync(
        string id,
        ISubZoneRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid sub zone id." });
        }

        var subZone = await repository.GetByIdAsync(id, cancellationToken);
        if (subZone is null)
        {
            return Results.NotFound();
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        if (!deleted)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("SubZone", subZone.Id, subZone.SubZoneName, EventAction.Deleted, cancellationToken);

        return Results.NoContent();
    }

    private static async Task<IResult> UploadSubZoneMapAsync(
        string id,
        IFormFile file,
        ISubZoneRepository repository,
        IMapFileStorageService mapFileStorageService,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid sub zone id." });
        }

        if (file.Length == 0)
        {
            return Results.BadRequest(new { message = "Map file is required." });
        }

        var subZone = await repository.GetByIdAsync(id, cancellationToken);
        if (subZone is null)
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

        subZone.MapPath = mapPath;

        var updated = await repository.UpdateAsync(id, subZone, cancellationToken);
        return updated ? Results.Ok(SubZoneResponse.FromEntity(subZone)) : Results.NotFound();
    }

    private static async Task<IResult> GetSubZoneMapAsync(
        string id,
        ISubZoneRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid sub zone id." });
        }

        var subZone = await repository.GetByIdAsync(id, cancellationToken);
        if (subZone is null || string.IsNullOrEmpty(subZone.MapPath))
        {
            return Results.NotFound();
        }

        return Results.Ok(new SubZoneMapResponse(subZone.Id, subZone.MapPath));
    }
}
