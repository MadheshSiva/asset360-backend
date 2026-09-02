using A360.Asset.Repository.Repositories;
using A360.MasterManagement.Api.Contracts;
using A360.MasterManagement.Api.Validation;
using A360.MasterManagement.Repository.Repositories;
using A360.Repository.Repositories;
using A360.Repository.Sequences;

namespace A360.MasterManagement.Api.Endpoints;

public static class ResolutionStatusEndpoints
{
    private const string SequenceName = "resolution_status";
    private const string StatusIdPrefix = "RES";

    public static RouteGroupBuilder MapResolutionStatusEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/resolution-statuses").WithTags("ResolutionStatuses");

        group.MapGet("", GetResolutionStatusesAsync).WithName("GetResolutionStatuses");
        group.MapGet("/{id}", GetResolutionStatusByIdAsync).WithName("GetResolutionStatusById");
        group.MapPost("", CreateResolutionStatusAsync).WithName("CreateResolutionStatus");
        group.MapPut("/{id}", UpdateResolutionStatusAsync).WithName("UpdateResolutionStatus");
        group.MapDelete("/{id}", DeleteResolutionStatusAsync).WithName("DeleteResolutionStatus");

        return group;
    }

    private static async Task<IResult> GetResolutionStatusesAsync(
        IResolutionStatusRepository repository,
        CancellationToken cancellationToken)
    {
        var resolutionStatuses = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(resolutionStatuses.Select(ResolutionStatusResponse.FromEntity));
    }

    private static async Task<IResult> GetResolutionStatusByIdAsync(
        string id,
        IResolutionStatusRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid resolution status id." });
        }

        var resolutionStatus = await repository.GetByIdAsync(id, cancellationToken);
        return resolutionStatus is null ? Results.NotFound() : Results.Ok(ResolutionStatusResponse.FromEntity(resolutionStatus));
    }

    private static async Task<IResult> CreateResolutionStatusAsync(
        CreateResolutionStatusRequest request,
        IResolutionStatusRepository repository,
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
        var statusId = $"{StatusIdPrefix}{nextSequence:D6}";

        var resolutionStatus = await repository.CreateAsync(
            request.ToEntity(statusId, asset.AssetName),
            cancellationToken);

        return Results.Created($"/api/resolution-statuses/{resolutionStatus.Id}", ResolutionStatusResponse.FromEntity(resolutionStatus));
    }

    private static async Task<IResult> UpdateResolutionStatusAsync(
        string id,
        UpdateResolutionStatusRequest request,
        IResolutionStatusRepository repository,
        IAssetRepository assetRepository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid resolution status id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var resolutionStatus = await repository.GetByIdAsync(id, cancellationToken);
        if (resolutionStatus is null)
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

        request.ApplyTo(resolutionStatus, asset.AssetName);

        var updated = await repository.UpdateAsync(id, resolutionStatus, cancellationToken);
        return updated ? Results.Ok(ResolutionStatusResponse.FromEntity(resolutionStatus)) : Results.NotFound();
    }

    private static async Task<IResult> DeleteResolutionStatusAsync(
        string id,
        IResolutionStatusRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid resolution status id." });
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
