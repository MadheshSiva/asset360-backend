using A360.Project.Api.Contracts;
using A360.Project.Api.Services;
using A360.Project.Api.Validation;
using A360.Project.Repository.Repositories;
using A360.Repository.Repositories;

namespace A360.Project.Api.Endpoints;

public static class FloorEndpoints
{
    private const string MapCategory = "floormaps";

    public static RouteGroupBuilder MapFloorEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/floors").WithTags("Floors");

        group.MapGet("", GetFloorsAsync).WithName("GetFloors");
        group.MapGet("/{id}", GetFloorByIdAsync).WithName("GetFloorById");
        group.MapPost("", CreateFloorAsync).WithName("CreateFloor");
        group.MapPut("/{id}", UpdateFloorAsync).WithName("UpdateFloor");
        group.MapDelete("/{id}", DeleteFloorAsync).WithName("DeleteFloor");
        group.MapPost("/{id}/map", UploadFloorMapAsync).WithName("UploadFloorMap").DisableAntiforgery();
        group.MapGet("/{id}/map", GetFloorMapAsync).WithName("GetFloorMap");

        return group;
    }

    private static async Task<IResult> GetFloorsAsync(
        IFloorRepository repository,
        CancellationToken cancellationToken)
    {
        var floors = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(floors.Select(FloorResponse.FromEntity));
    }

    private static async Task<IResult> GetFloorByIdAsync(
        string id,
        IFloorRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid floor id." });
        }

        var floor = await repository.GetByIdAsync(id, cancellationToken);
        return floor is null ? Results.NotFound() : Results.Ok(FloorResponse.FromEntity(floor));
    }

    private static async Task<IResult> CreateFloorAsync(
        CreateFloorRequest request,
        IFloorRepository repository,
        IProjectRepository projectRepository,
        ICountryRepository countryRepository,
        IAreaRepository areaRepository,
        IBuildingRepository buildingRepository,
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
            cancellationToken);
        if (relationshipErrors.Count > 0)
        {
            return Results.ValidationProblem(relationshipErrors);
        }

        var floor = await repository.CreateAsync(request.ToEntity(), cancellationToken);
        return Results.Created($"/api/floors/{floor.Id}", FloorResponse.FromEntity(floor));
    }

    private static async Task<IResult> UpdateFloorAsync(
        string id,
        UpdateFloorRequest request,
        IFloorRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid floor id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var floor = await repository.GetByIdAsync(id, cancellationToken);
        if (floor is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(floor);

        var updated = await repository.UpdateAsync(id, floor, cancellationToken);
        return updated ? Results.Ok(FloorResponse.FromEntity(floor)) : Results.NotFound();
    }

    private static async Task<IResult> DeleteFloorAsync(
        string id,
        IFloorRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid floor id." });
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UploadFloorMapAsync(
        string id,
        IFormFile file,
        IFloorRepository repository,
        IMapFileStorageService mapFileStorageService,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid floor id." });
        }

        if (file.Length == 0)
        {
            return Results.BadRequest(new { message = "Map file is required." });
        }

        var floor = await repository.GetByIdAsync(id, cancellationToken);
        if (floor is null)
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

        floor.MapPath = mapPath;

        var updated = await repository.UpdateAsync(id, floor, cancellationToken);
        return updated ? Results.Ok(FloorResponse.FromEntity(floor)) : Results.NotFound();
    }

    private static async Task<IResult> GetFloorMapAsync(
        string id,
        IFloorRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid floor id." });
        }

        var floor = await repository.GetByIdAsync(id, cancellationToken);
        if (floor is null || string.IsNullOrEmpty(floor.MapPath))
        {
            return Results.NotFound();
        }

        return Results.Ok(new FloorMapResponse(floor.Id, floor.MapPath));
    }
}
