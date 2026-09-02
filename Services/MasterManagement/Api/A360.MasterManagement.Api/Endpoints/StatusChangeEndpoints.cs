using A360.Asset.Repository.Repositories;
using A360.MasterManagement.Api.Contracts;
using A360.MasterManagement.Api.Validation;
using A360.MasterManagement.Repository.Repositories;
using A360.Repository.Repositories;
using A360.Repository.Sequences;

namespace A360.MasterManagement.Api.Endpoints;

public static class StatusChangeEndpoints
{
    private const string SequenceName = "status_change";
    private const string StatusChangeIdPrefix = "STC";

    public static RouteGroupBuilder MapStatusChangeEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/status-changes").WithTags("StatusChanges");

        group.MapGet("", GetStatusChangesAsync).WithName("GetStatusChanges");
        group.MapGet("/{id}", GetStatusChangeByIdAsync).WithName("GetStatusChangeById");
        group.MapPost("", CreateStatusChangeAsync).WithName("CreateStatusChange");
        group.MapPut("/{id}", UpdateStatusChangeAsync).WithName("UpdateStatusChange");
        group.MapDelete("/{id}", DeleteStatusChangeAsync).WithName("DeleteStatusChange");

        return group;
    }

    private static async Task<IResult> GetStatusChangesAsync(
        IStatusChangeRepository repository,
        CancellationToken cancellationToken)
    {
        var statusChanges = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(statusChanges.Select(StatusChangeResponse.FromEntity));
    }

    private static async Task<IResult> GetStatusChangeByIdAsync(
        string id,
        IStatusChangeRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid status change id." });
        }

        var statusChange = await repository.GetByIdAsync(id, cancellationToken);
        return statusChange is null ? Results.NotFound() : Results.Ok(StatusChangeResponse.FromEntity(statusChange));
    }

    private static async Task<IResult> CreateStatusChangeAsync(
        CreateStatusChangeRequest request,
        IStatusChangeRepository repository,
        IAssetRepository assetRepository,
        ISequenceGenerator sequenceGenerator,
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
        var statusChangeId = $"{StatusChangeIdPrefix}{nextSequence:D6}";

        var statusChange = await repository.CreateAsync(
            request.ToEntity(statusChangeId, asset.AssetName),
            cancellationToken);

        return Results.Created($"/api/status-changes/{statusChange.Id}", StatusChangeResponse.FromEntity(statusChange));
    }

    private static async Task<IResult> UpdateStatusChangeAsync(
        string id,
        UpdateStatusChangeRequest request,
        IStatusChangeRepository repository,
        IAssetRepository assetRepository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid status change id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var statusChange = await repository.GetByIdAsync(id, cancellationToken);
        if (statusChange is null)
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

        request.ApplyTo(statusChange, asset.AssetName);

        var updated = await repository.UpdateAsync(id, statusChange, cancellationToken);
        return updated ? Results.Ok(StatusChangeResponse.FromEntity(statusChange)) : Results.NotFound();
    }

    private static async Task<IResult> DeleteStatusChangeAsync(
        string id,
        IStatusChangeRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid status change id." });
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
