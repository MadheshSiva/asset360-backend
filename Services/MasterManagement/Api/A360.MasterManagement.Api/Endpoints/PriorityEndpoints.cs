using A360.Asset.Repository.Repositories;
using A360.Domain.Entities;
using A360.MasterManagement.Api.Contracts;
using A360.MasterManagement.Api.Validation;
using A360.MasterManagement.Repository.Repositories;
using A360.Repository.Activity;
using A360.Repository.Repositories;
using A360.Repository.Sequences;

namespace A360.MasterManagement.Api.Endpoints;

public static class PriorityEndpoints
{
    private const string SequenceName = "priority";
    private const string PriorityIdPrefix = "PRI";

    public static RouteGroupBuilder MapPriorityEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/priorities").WithTags("Priorities");

        group.MapGet("", GetPrioritiesAsync).WithName("GetPriorities");
        group.MapGet("/{id}", GetPriorityByIdAsync).WithName("GetPriorityById");
        group.MapPost("", CreatePriorityAsync).WithName("CreatePriority");
        group.MapPut("/{id}", UpdatePriorityAsync).WithName("UpdatePriority");
        group.MapDelete("/{id}", DeletePriorityAsync).WithName("DeletePriority");

        return group;
    }

    private static async Task<IResult> GetPrioritiesAsync(
        IPriorityRepository repository,
        CancellationToken cancellationToken)
    {
        var priorities = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(priorities.Select(PriorityResponse.FromEntity));
    }

    private static async Task<IResult> GetPriorityByIdAsync(
        string id,
        IPriorityRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid priority id." });
        }

        var priority = await repository.GetByIdAsync(id, cancellationToken);
        if (priority is null)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("Priority", priority.PriorityId, priority.PriorityName, EventAction.Viewed, cancellationToken);
        return Results.Ok(PriorityResponse.FromEntity(priority));
    }

    private static async Task<IResult> CreatePriorityAsync(
        CreatePriorityRequest request,
        IPriorityRepository repository,
        IAssetRepository assetRepository,
        ISequenceGenerator sequenceGenerator,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var asset = await assetRepository.GetByAssetIdAsync(request.AssetId!, cancellationToken);
        if (asset is null)
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                ["AssetId"] = ["No asset exists with this AssetId"]
            });
        }

        var nextSequence = await sequenceGenerator.GetNextValueAsync(SequenceName, cancellationToken);
        var priorityId = $"{PriorityIdPrefix}{nextSequence:D6}";

        var priority = await repository.CreateAsync(
            request.ToEntity(priorityId, asset.AssetName),
            cancellationToken);

        await eventLogger.LogAsync("Priority", priority.PriorityId, priority.PriorityName, EventAction.Created, cancellationToken);

        return Results.Created($"/api/priorities/{priority.Id}", PriorityResponse.FromEntity(priority));
    }

    private static async Task<IResult> UpdatePriorityAsync(
        string id,
        UpdatePriorityRequest request,
        IPriorityRepository repository,
        IAssetRepository assetRepository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid priority id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var priority = await repository.GetByIdAsync(id, cancellationToken);
        if (priority is null)
        {
            return Results.NotFound();
        }

        var asset = await assetRepository.GetByAssetIdAsync(request.AssetId!, cancellationToken);
        if (asset is null)
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                ["AssetId"] = ["No asset exists with this AssetId"]
            });
        }

        request.ApplyTo(priority, asset.AssetName);

        var updated = await repository.UpdateAsync(id, priority, cancellationToken);
        if (!updated)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("Priority", priority.PriorityId, priority.PriorityName, EventAction.Updated, cancellationToken);
        return Results.Ok(PriorityResponse.FromEntity(priority));
    }

    private static async Task<IResult> DeletePriorityAsync(
        string id,
        IPriorityRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid priority id." });
        }

        var priority = await repository.GetByIdAsync(id, cancellationToken);
        if (priority is null)
        {
            return Results.NotFound();
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        if (!deleted)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("Priority", priority.PriorityId, priority.PriorityName, EventAction.Deleted, cancellationToken);
        return Results.NoContent();
    }
}
