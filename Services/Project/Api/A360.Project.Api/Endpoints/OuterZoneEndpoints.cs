using A360.Project.Api.Contracts;
using A360.Project.Api.Validation;
using A360.Project.Repository.Repositories;
using A360.Repository.Repositories;

namespace A360.Project.Api.Endpoints;

public static class OuterZoneEndpoints
{
    public static RouteGroupBuilder MapOuterZoneEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/outer-zones").WithTags("Outer Zones");

        group.MapGet("", GetOuterZonesAsync).WithName("GetOuterZones");
        group.MapGet("/{id}", GetOuterZoneByIdAsync).WithName("GetOuterZoneById");
        group.MapPost("", CreateOuterZoneAsync).WithName("CreateOuterZone");
        group.MapPut("/{id}", UpdateOuterZoneAsync).WithName("UpdateOuterZone");
        group.MapDelete("/{id}", DeleteOuterZoneAsync).WithName("DeleteOuterZone");

        return group;
    }

    private static async Task<IResult> GetOuterZonesAsync(
        IOuterZoneRepository repository,
        CancellationToken cancellationToken)
    {
        var outerZones = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(outerZones.Select(OuterZoneResponse.FromEntity));
    }

    private static async Task<IResult> GetOuterZoneByIdAsync(
        string id,
        IOuterZoneRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid outer zone id." });
        }

        var outerZone = await repository.GetByIdAsync(id, cancellationToken);
        return outerZone is null ? Results.NotFound() : Results.Ok(OuterZoneResponse.FromEntity(outerZone));
    }

    private static async Task<IResult> CreateOuterZoneAsync(
        CreateOuterZoneRequest request,
        IOuterZoneRepository repository,
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

        var outerZone = await repository.CreateAsync(request.ToEntity(), cancellationToken);
        return Results.Created($"/api/outer-zones/{outerZone.Id}", OuterZoneResponse.FromEntity(outerZone));
    }

    private static async Task<IResult> UpdateOuterZoneAsync(
        string id,
        UpdateOuterZoneRequest request,
        IOuterZoneRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid outer zone id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var outerZone = await repository.GetByIdAsync(id, cancellationToken);
        if (outerZone is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(outerZone);

        var updated = await repository.UpdateAsync(id, outerZone, cancellationToken);
        return updated ? Results.Ok(OuterZoneResponse.FromEntity(outerZone)) : Results.NotFound();
    }

    private static async Task<IResult> DeleteOuterZoneAsync(
        string id,
        IOuterZoneRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid outer zone id." });
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
