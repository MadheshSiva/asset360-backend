using A360.Asset.Repository.Repositories;
using A360.Domain.Entities;
using A360.MasterManagement.Api.Contracts;
using A360.MasterManagement.Api.Validation;
using A360.MasterManagement.Repository.Repositories;
using A360.Repository.Activity;
using A360.Repository.Repositories;
using A360.Repository.Sequences;

namespace A360.MasterManagement.Api.Endpoints;

public static class PhysicalVerificationResultEndpoints
{
    private const string SequenceName = "physical_verification_result";
    private const string ResultIdPrefix = "PVR";

    public static RouteGroupBuilder MapPhysicalVerificationResultEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/physical-verification-results").WithTags("PhysicalVerificationResults");

        group.MapGet("", GetPhysicalVerificationResultsAsync).WithName("GetPhysicalVerificationResults");
        group.MapGet("/{id}", GetPhysicalVerificationResultByIdAsync).WithName("GetPhysicalVerificationResultById");
        group.MapPost("", CreatePhysicalVerificationResultAsync).WithName("CreatePhysicalVerificationResult");
        group.MapPut("/{id}", UpdatePhysicalVerificationResultAsync).WithName("UpdatePhysicalVerificationResult");
        group.MapDelete("/{id}", DeletePhysicalVerificationResultAsync).WithName("DeletePhysicalVerificationResult");

        return group;
    }

    private static async Task<IResult> GetPhysicalVerificationResultsAsync(
        IPhysicalVerificationResultRepository repository,
        CancellationToken cancellationToken)
    {
        var physicalVerificationResults = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(physicalVerificationResults.Select(PhysicalVerificationResultResponse.FromEntity));
    }

    private static async Task<IResult> GetPhysicalVerificationResultByIdAsync(
        string id,
        IPhysicalVerificationResultRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid physical verification result id." });
        }

        var physicalVerificationResult = await repository.GetByIdAsync(id, cancellationToken);
        if (physicalVerificationResult is null)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("PhysicalVerificationResult", physicalVerificationResult.ResultId, physicalVerificationResult.ResultName, EventAction.Viewed, cancellationToken);
        return Results.Ok(PhysicalVerificationResultResponse.FromEntity(physicalVerificationResult));
    }

    private static async Task<IResult> CreatePhysicalVerificationResultAsync(
        CreatePhysicalVerificationResultRequest request,
        IPhysicalVerificationResultRepository repository,
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
        var resultId = $"{ResultIdPrefix}{nextSequence:D6}";

        var physicalVerificationResult = await repository.CreateAsync(
            request.ToEntity(resultId, asset.AssetName),
            cancellationToken);

        await eventLogger.LogAsync("PhysicalVerificationResult", physicalVerificationResult.ResultId, physicalVerificationResult.ResultName, EventAction.Created, cancellationToken);

        return Results.Created($"/api/physical-verification-results/{physicalVerificationResult.Id}", PhysicalVerificationResultResponse.FromEntity(physicalVerificationResult));
    }

    private static async Task<IResult> UpdatePhysicalVerificationResultAsync(
        string id,
        UpdatePhysicalVerificationResultRequest request,
        IPhysicalVerificationResultRepository repository,
        IAssetRepository assetRepository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid physical verification result id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var physicalVerificationResult = await repository.GetByIdAsync(id, cancellationToken);
        if (physicalVerificationResult is null)
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

        request.ApplyTo(physicalVerificationResult, asset.AssetName);

        var updated = await repository.UpdateAsync(id, physicalVerificationResult, cancellationToken);
        if (!updated)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("PhysicalVerificationResult", physicalVerificationResult.ResultId, physicalVerificationResult.ResultName, EventAction.Updated, cancellationToken);
        return Results.Ok(PhysicalVerificationResultResponse.FromEntity(physicalVerificationResult));
    }

    private static async Task<IResult> DeletePhysicalVerificationResultAsync(
        string id,
        IPhysicalVerificationResultRepository repository,
        IEventLogger eventLogger,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid physical verification result id." });
        }

        var physicalVerificationResult = await repository.GetByIdAsync(id, cancellationToken);
        if (physicalVerificationResult is null)
        {
            return Results.NotFound();
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        if (!deleted)
        {
            return Results.NotFound();
        }

        await eventLogger.LogAsync("PhysicalVerificationResult", physicalVerificationResult.ResultId, physicalVerificationResult.ResultName, EventAction.Deleted, cancellationToken);
        return Results.NoContent();
    }
}
