using A360.Asset.Api.Contracts;
using A360.Asset.Api.Validation;
using A360.Asset.Repository.Repositories;
using A360.Repository.Repositories;
using A360.Repository.Sequences;

namespace A360.Asset.Api.Endpoints;

public static class AssetUtilizationAndPerformanceEndpoints
{
    private const string SequenceName = "asset-utilization-and-performance";
    private const string UtilizationPerformanceIdPrefix = "AUP";

    public static RouteGroupBuilder MapAssetUtilizationAndPerformanceEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/asset-utilization-and-performance").WithTags("AssetUtilizationAndPerformance");

        group.MapGet("", GetAssetUtilizationAndPerformancesAsync).WithName("GetAssetUtilizationAndPerformances");
        group.MapGet("/{id}", GetAssetUtilizationAndPerformanceByIdAsync).WithName("GetAssetUtilizationAndPerformanceById");
        group.MapGet("/by-asset/{assetId}", GetAssetUtilizationAndPerformancesByAssetIdAsync).WithName("GetAssetUtilizationAndPerformancesByAssetId");
        group.MapPost("", CreateAssetUtilizationAndPerformanceAsync).WithName("CreateAssetUtilizationAndPerformance");
        group.MapPut("/{id}", UpdateAssetUtilizationAndPerformanceAsync).WithName("UpdateAssetUtilizationAndPerformance");
        group.MapDelete("/{id}", DeleteAssetUtilizationAndPerformanceAsync).WithName("DeleteAssetUtilizationAndPerformance");

        return group;
    }

    private static async Task<IResult> GetAssetUtilizationAndPerformancesAsync(
        IAssetUtilizationAndPerformanceRepository repository,
        CancellationToken cancellationToken)
    {
        var records = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(records.Select(AssetUtilizationAndPerformanceResponse.FromEntity));
    }

    private static async Task<IResult> GetAssetUtilizationAndPerformanceByIdAsync(
        string id,
        IAssetUtilizationAndPerformanceRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset utilization and performance id." });
        }

        var record = await repository.GetByIdAsync(id, cancellationToken);
        return record is null ? Results.NotFound() : Results.Ok(AssetUtilizationAndPerformanceResponse.FromEntity(record));
    }

    private static async Task<IResult> GetAssetUtilizationAndPerformancesByAssetIdAsync(
        string assetId,
        IAssetUtilizationAndPerformanceRepository repository,
        CancellationToken cancellationToken)
    {
        var records = await repository.GetByAssetIdAsync(assetId, cancellationToken);
        return Results.Ok(records.Select(AssetUtilizationAndPerformanceResponse.FromEntity));
    }

    private static async Task<IResult> CreateAssetUtilizationAndPerformanceAsync(
        CreateAssetUtilizationAndPerformanceRequest request,
        IAssetUtilizationAndPerformanceRepository repository,
        ISequenceGenerator sequenceGenerator,
        CancellationToken cancellationToken)
    {
        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var nextSequence = await sequenceGenerator.GetNextValueAsync(SequenceName, cancellationToken);
        var utilizationPerformanceId = $"{UtilizationPerformanceIdPrefix}{nextSequence:D6}";

        var record = await repository.CreateAsync(request.ToEntity(utilizationPerformanceId), cancellationToken);
        return Results.Created($"/api/asset-utilization-and-performance/{record.Id}", AssetUtilizationAndPerformanceResponse.FromEntity(record));
    }

    private static async Task<IResult> UpdateAssetUtilizationAndPerformanceAsync(
        string id,
        UpdateAssetUtilizationAndPerformanceRequest request,
        IAssetUtilizationAndPerformanceRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset utilization and performance id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var record = await repository.GetByIdAsync(id, cancellationToken);
        if (record is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(record);

        var updated = await repository.UpdateAsync(id, record, cancellationToken);
        return updated ? Results.Ok(AssetUtilizationAndPerformanceResponse.FromEntity(record)) : Results.NotFound();
    }

    private static async Task<IResult> DeleteAssetUtilizationAndPerformanceAsync(
        string id,
        IAssetUtilizationAndPerformanceRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset utilization and performance id." });
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
