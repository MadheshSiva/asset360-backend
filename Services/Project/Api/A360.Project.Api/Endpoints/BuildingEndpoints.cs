using A360.Domain.Entities;
using A360.Project.Api.Contracts;
using A360.Project.Api.Validation;
using A360.Project.Repository.Repositories;
using A360.Repository.Activity;
using A360.Repository.Repositories;

namespace A360.Project.Api.Endpoints;

public static class BuildingEndpoints
{
    public static RouteGroupBuilder MapBuildingEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/buildings").WithTags("Buildings");

        group.MapGet("", GetBuildingsAsync).WithName("GetBuildings");
        group.MapGet("/{id}", GetBuildingByIdAsync).WithName("GetBuildingById");
        group.MapPost("", CreateBuildingAsync).WithName("CreateBuilding");
        group.MapPut("/{id}", UpdateBuildingAsync).WithName("UpdateBuilding");
        group.MapDelete("/{id}", DeleteBuildingAsync).WithName("DeleteBuilding");

        return group;
    }

    private static async Task<IResult> GetBuildingsAsync(
        IBuildingRepository repository,
        CancellationToken cancellationToken)
    {
        var buildings = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(buildings.Select(BuildingResponse.FromEntity));
    }

    private static async Task<IResult> GetBuildingByIdAsync(
        string id,
        IBuildingRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid building id." });
        }

        var building = await repository.GetByIdAsync(id, cancellationToken);
        if (building is null)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("Building", building.Id, building.BuildingName, EventAction.Viewed, cancellationToken);

        return Results.Ok(BuildingResponse.FromEntity(building));
    }

    private static async Task<IResult> CreateBuildingAsync(
        CreateBuildingRequest request,
        IBuildingRepository repository,
        IProjectRepository projectRepository,
        ICountryRepository countryRepository,
        IAreaRepository areaRepository,
        IOuterZoneRepository outerZoneRepository,
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
            cancellationToken);
        if (relationshipErrors.Count > 0)
        {
            return Results.ValidationProblem(relationshipErrors);
        }

        var building = await repository.CreateAsync(request.ToEntity(), cancellationToken);

        await eventLogger.LogAsync("Building", building.Id, building.BuildingName, EventAction.Created, cancellationToken);

        return Results.Created($"/api/buildings/{building.Id}", BuildingResponse.FromEntity(building));
    }

    private static async Task<IResult> UpdateBuildingAsync(
        string id,
        UpdateBuildingRequest request,
        IBuildingRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid building id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var building = await repository.GetByIdAsync(id, cancellationToken);
        if (building is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(building);

        var updated = await repository.UpdateAsync(id, building, cancellationToken);
        if (!updated)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("Building", building.Id, building.BuildingName, EventAction.Updated, cancellationToken);

        return Results.Ok(BuildingResponse.FromEntity(building));
    }

    private static async Task<IResult> DeleteBuildingAsync(
        string id,
        IBuildingRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid building id." });
        }

        var building = await repository.GetByIdAsync(id, cancellationToken);
        if (building is null)
        {
            return Results.NotFound();
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        if (!deleted)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("Building", building.Id, building.BuildingName, EventAction.Deleted, cancellationToken);

        return Results.NoContent();
    }
}
