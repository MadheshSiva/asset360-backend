using A360.Project.Api.Contracts;
using A360.Project.Api.Validation;
using A360.Project.Repository.Repositories;
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
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid building id." });
        }

        var building = await repository.GetByIdAsync(id, cancellationToken);
        return building is null ? Results.NotFound() : Results.Ok(BuildingResponse.FromEntity(building));
    }

    private static async Task<IResult> CreateBuildingAsync(
        CreateBuildingRequest request,
        IBuildingRepository repository,
        IProjectRepository projectRepository,
        ICountryRepository countryRepository,
        IAreaRepository areaRepository,
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
            cancellationToken);
        if (relationshipErrors.Count > 0)
        {
            return Results.ValidationProblem(relationshipErrors);
        }

        var building = await repository.CreateAsync(request.ToEntity(), cancellationToken);
        return Results.Created($"/api/buildings/{building.Id}", BuildingResponse.FromEntity(building));
    }

    private static async Task<IResult> UpdateBuildingAsync(
        string id,
        UpdateBuildingRequest request,
        IBuildingRepository repository,
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
        return updated ? Results.Ok(BuildingResponse.FromEntity(building)) : Results.NotFound();
    }

    private static async Task<IResult> DeleteBuildingAsync(
        string id,
        IBuildingRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid building id." });
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
