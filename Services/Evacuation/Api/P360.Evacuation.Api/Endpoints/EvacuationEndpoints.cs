
using P360.Evacuation.Api.Contracts;
using P360.Evacuation.Api.Validation;
using P360.Evacuation.Repository.Repositories;
using P360.Repository.Repositories;

namespace P360.Evacuation.Api.Endpoints;

public static class EvacuationEndpoints
{
    public static RouteGroupBuilder MapEvacuationEndpoints(
        this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/evacuations")
            .WithTags("Evacuations");

        group.MapGet("", GetEvacuationsAsync)
            .WithName("GetEvacuations");

        group.MapGet("/{id}", GetEvacuationByIdAsync)
            .WithName("GetEvacuationById");

        group.MapPost("", CreateEvacuationAsync)
            .WithName("CreateEvacuation");

        group.MapPut("/{id}", UpdateEvacuationAsync)
            .WithName("UpdateEvacuation");

        group.MapDelete("/{id}", DeleteEvacuationAsync)
            .WithName("DeleteEvacuation");

        return group;
    }

    private static async Task<IResult> GetEvacuationsAsync(
        IEvacuationRepository repository,
        CancellationToken cancellationToken)
    {
        var evacuations = await repository.GetAllAsync(
            cancellationToken);

        return Results.Ok(
            evacuations.Select(EvacuationResponse.FromEntity));
    }

    private static async Task<IResult> GetEvacuationByIdAsync(
        string id,
        IEvacuationRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid evacuation id." });
        }

        var evacuation = await repository.GetByIdAsync(
            id,
            cancellationToken);

        return evacuation is null
            ? Results.NotFound()
            : Results.Ok(EvacuationResponse.FromEntity(evacuation));
    }

    private static async Task<IResult> CreateEvacuationAsync(
        CreateEvacuationRequest request,
        IEvacuationRepository repository,
        CancellationToken cancellationToken)
    {
        var validationErrors = request.Validate();

        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var evacuation = await repository.CreateAsync(
            request.ToEntity(),
            cancellationToken);

        return Results.Created(
            $"/api/evacuations/{evacuation.Id}",
            EvacuationResponse.FromEntity(evacuation));
    }

    private static async Task<IResult> UpdateEvacuationAsync(
        string id,
        UpdateEvacuationRequest request,
        IEvacuationRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid evacuation id." });
        }

        var validationErrors = request.Validate();

        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var evacuation = await repository.GetByIdAsync(
            id,
            cancellationToken);

        if (evacuation is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(evacuation);

        var updated = await repository.UpdateAsync(
            id,
            evacuation,
            cancellationToken);

        return updated
            ? Results.Ok(EvacuationResponse.FromEntity(evacuation))
            : Results.NotFound();
    }

    private static async Task<IResult> DeleteEvacuationAsync(
        string id,
        IEvacuationRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid evacuation id." });
        }

        var deleted = await repository.DeleteAsync(
            id,
            cancellationToken);

        return deleted
            ? Results.NoContent()
            : Results.NotFound();
    }
}
