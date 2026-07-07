
using A360.Evacuation.Api.Contracts;
using A360.Evacuation.Api.Validation;
using A360.Evacuation.Repository.Repositories;
using A360.Repository.Repositories;

namespace A360.Evacuation.Api.Endpoints;

public static class EvacuationTriggerEndpoints
{
    public static RouteGroupBuilder MapEvacuationTriggerEndpoints(
        this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/evacuationtriggers")
            .WithTags("Evacuation Triggers");

        group.MapGet("", GetEvacuationTriggersAsync)
            .WithName("GetEvacuationTriggers");

        group.MapGet("/{id}", GetEvacuationTriggerByIdAsync)
            .WithName("GetEvacuationTriggerById");

        group.MapPost("", CreateEvacuationTriggerAsync)
            .WithName("CreateEvacuationTrigger");

        group.MapPut("/{id}", UpdateEvacuationTriggerAsync)
            .WithName("UpdateEvacuationTrigger");

        group.MapDelete("/{id}", DeleteEvacuationTriggerAsync)
            .WithName("DeleteEvacuationTrigger");

        return group;
    }

    private static async Task<IResult> GetEvacuationTriggersAsync(
        IEvacuationTriggerRepository repository,
        CancellationToken cancellationToken)
    {
        var evacuationTriggers = await repository.GetAllAsync(
            cancellationToken);

        return Results.Ok(
            evacuationTriggers.Select(EvacuationTriggerResponse.FromEntity));
    }

    private static async Task<IResult> GetEvacuationTriggerByIdAsync(
        string id,
        IEvacuationTriggerRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid evacuation trigger id." });
        }

        var evacuationTrigger = await repository.GetByIdAsync(
            id,
            cancellationToken);

        return evacuationTrigger is null
            ? Results.NotFound()
            : Results.Ok(EvacuationTriggerResponse.FromEntity(evacuationTrigger));
    }

    private static async Task<IResult> CreateEvacuationTriggerAsync(
        CreateEvacuationTriggerRequest request,
        IEvacuationTriggerRepository repository,
        CancellationToken cancellationToken)
    {
        var validationErrors = request.Validate();

        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var evacuationTrigger = await repository.CreateAsync(
            request.ToEntity(),
            cancellationToken);

        return Results.Created(
            $"/api/evacuationtriggers/{evacuationTrigger.Id}",
            EvacuationTriggerResponse.FromEntity(evacuationTrigger));
    }

    private static async Task<IResult> UpdateEvacuationTriggerAsync(
        string id,
        UpdateEvacuationTriggerRequest request,
        IEvacuationTriggerRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid evacuation trigger id." });
        }

        var validationErrors = request.Validate();

        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var evacuationTrigger = await repository.GetByIdAsync(
            id,
            cancellationToken);

        if (evacuationTrigger is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(evacuationTrigger);

        var updated = await repository.UpdateAsync(
            id,
            evacuationTrigger,
            cancellationToken);

        return updated
            ? Results.Ok(EvacuationTriggerResponse.FromEntity(evacuationTrigger))
            : Results.NotFound();
    }

    private static async Task<IResult> DeleteEvacuationTriggerAsync(
        string id,
        IEvacuationTriggerRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(
                new { message = "Invalid evacuation trigger id." });
        }

        var deleted = await repository.DeleteAsync(
            id,
            cancellationToken);

        return deleted
            ? Results.NoContent()
            : Results.NotFound();
    }
}
