using A360.Asset.Api.Contracts;
using A360.Asset.Api.Validation;
using A360.Asset.Repository.Repositories;
using A360.Repository.Repositories;
using A360.Repository.Sequences;

namespace A360.Asset.Api.Endpoints;

public static class AssetFinancialDetailsEndpoints
{
    private const string SequenceName = "asset-financial-details";
    private const string FinancialDetailsIdPrefix = "AFD";

    public static RouteGroupBuilder MapAssetFinancialDetailsEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/asset-financial-details").WithTags("AssetFinancialDetails");

        group.MapGet("", GetAssetFinancialDetailsListAsync).WithName("GetAssetFinancialDetailsList");
        group.MapGet("/{id}", GetAssetFinancialDetailsByIdAsync).WithName("GetAssetFinancialDetailsById");
        group.MapGet("/by-asset/{assetId}", GetAssetFinancialDetailsByAssetIdAsync).WithName("GetAssetFinancialDetailsByAssetId");
        group.MapPost("", CreateAssetFinancialDetailsAsync).WithName("CreateAssetFinancialDetails");
        group.MapPut("/{id}", UpdateAssetFinancialDetailsAsync).WithName("UpdateAssetFinancialDetails");
        group.MapDelete("/{id}", DeleteAssetFinancialDetailsAsync).WithName("DeleteAssetFinancialDetails");

        return group;
    }

    private static async Task<IResult> GetAssetFinancialDetailsListAsync(
        IAssetFinancialDetailsRepository repository,
        CancellationToken cancellationToken)
    {
        var records = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(records.Select(AssetFinancialDetailsResponse.FromEntity));
    }

    private static async Task<IResult> GetAssetFinancialDetailsByIdAsync(
        string id,
        IAssetFinancialDetailsRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset financial details id." });
        }

        var record = await repository.GetByIdAsync(id, cancellationToken);
        return record is null ? Results.NotFound() : Results.Ok(AssetFinancialDetailsResponse.FromEntity(record));
    }

    private static async Task<IResult> GetAssetFinancialDetailsByAssetIdAsync(
        string assetId,
        IAssetFinancialDetailsRepository repository,
        CancellationToken cancellationToken)
    {
        var records = await repository.GetByAssetIdAsync(assetId, cancellationToken);
        return Results.Ok(records.Select(AssetFinancialDetailsResponse.FromEntity));
    }

    private static async Task<IResult> CreateAssetFinancialDetailsAsync(
        CreateAssetFinancialDetailsRequest request,
        IAssetFinancialDetailsRepository repository,
        ISequenceGenerator sequenceGenerator,
        CancellationToken cancellationToken)
    {
        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var nextSequence = await sequenceGenerator.GetNextValueAsync(SequenceName, cancellationToken);
        var financialDetailsId = $"{FinancialDetailsIdPrefix}{nextSequence:D6}";

        var record = await repository.CreateAsync(request.ToEntity(financialDetailsId), cancellationToken);
        return Results.Created($"/api/asset-financial-details/{record.Id}", AssetFinancialDetailsResponse.FromEntity(record));
    }

    private static async Task<IResult> UpdateAssetFinancialDetailsAsync(
        string id,
        UpdateAssetFinancialDetailsRequest request,
        IAssetFinancialDetailsRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset financial details id." });
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
        return updated ? Results.Ok(AssetFinancialDetailsResponse.FromEntity(record)) : Results.NotFound();
    }

    private static async Task<IResult> DeleteAssetFinancialDetailsAsync(
        string id,
        IAssetFinancialDetailsRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset financial details id." });
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
