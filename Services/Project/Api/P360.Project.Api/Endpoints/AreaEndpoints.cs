using P360.Project.Api.Contracts;
using P360.Project.Api.Validation;
using P360.Project.Repository.Repositories;
using P360.Repository.Repositories;

namespace P360.Project.Api.Endpoints;

public static class AreaEndpoints
{
    public static RouteGroupBuilder MapAreaEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/areas").WithTags("Areas");

        group.MapGet("", GetAreasAsync).WithName("GetAreas");
        group.MapGet("/{id}", GetAreaByIdAsync).WithName("GetAreaById");
        group.MapPost("", CreateAreaAsync).WithName("CreateArea");
        group.MapPut("/{id}", UpdateAreaAsync).WithName("UpdateArea");
        group.MapDelete("/{id}", DeleteAreaAsync).WithName("DeleteArea");

        return group;
    }

    private static async Task<IResult> GetAreasAsync(
        IAreaRepository repository,
        CancellationToken cancellationToken)
    {
        var areas = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(areas.Select(AreaResponse.FromEntity));
    }

    private static async Task<IResult> GetAreaByIdAsync(
        string id,
        IAreaRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid area id." });
        }

        var area = await repository.GetByIdAsync(id, cancellationToken);
        return area is null ? Results.NotFound() : Results.Ok(AreaResponse.FromEntity(area));
    }

    private static async Task<IResult> CreateAreaAsync(
        CreateAreaRequest request,
        IAreaRepository repository,
        IProjectRepository projectRepository,
        ICountryRepository countryRepository,
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
            cancellationToken);
        if (relationshipErrors.Count > 0)
        {
            return Results.ValidationProblem(relationshipErrors);
        }

        var area = await repository.CreateAsync(request.ToEntity(), cancellationToken);
        return Results.Created($"/api/areas/{area.Id}", AreaResponse.FromEntity(area));
    }

    private static async Task<IResult> UpdateAreaAsync(
        string id,
        UpdateAreaRequest request,
        IAreaRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid area id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var area = await repository.GetByIdAsync(id, cancellationToken);
        if (area is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(area);

        var updated = await repository.UpdateAsync(id, area, cancellationToken);
        return updated ? Results.Ok(AreaResponse.FromEntity(area)) : Results.NotFound();
    }

    private static async Task<IResult> DeleteAreaAsync(
        string id,
        IAreaRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid area id." });
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
