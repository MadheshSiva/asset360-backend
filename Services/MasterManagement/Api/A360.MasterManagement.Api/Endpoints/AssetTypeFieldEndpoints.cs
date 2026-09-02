using A360.Asset.Repository.Repositories;
using A360.MasterManagement.Api.Contracts;
using A360.MasterManagement.Api.Validation;
using A360.MasterManagement.Repository.Repositories;
using A360.Repository.Repositories;
using A360.Repository.Sequences;

namespace A360.MasterManagement.Api.Endpoints;

public static class AssetTypeFieldEndpoints
{
    private const string SequenceName = "asset_type_field";
    private const string FieldIdPrefix = "ATF";

    public static RouteGroupBuilder MapAssetTypeFieldEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/asset-type-fields").WithTags("AssetTypeFields");

        group.MapGet("", GetAssetTypeFieldsAsync).WithName("GetAssetTypeFields");
        group.MapGet("/{id}", GetAssetTypeFieldByIdAsync).WithName("GetAssetTypeFieldById");
        group.MapPost("", CreateAssetTypeFieldAsync).WithName("CreateAssetTypeField");
        group.MapPut("/{id}", UpdateAssetTypeFieldAsync).WithName("UpdateAssetTypeField");
        group.MapDelete("/{id}", DeleteAssetTypeFieldAsync).WithName("DeleteAssetTypeField");

        return group;
    }

    private static async Task<IResult> GetAssetTypeFieldsAsync(
        IAssetTypeFieldRepository repository,
        CancellationToken cancellationToken)
    {
        var assetTypeFields = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(assetTypeFields.Select(AssetTypeFieldResponse.FromEntity));
    }

    private static async Task<IResult> GetAssetTypeFieldByIdAsync(
        string id,
        IAssetTypeFieldRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset type field id." });
        }

        var assetTypeField = await repository.GetByIdAsync(id, cancellationToken);
        return assetTypeField is null ? Results.NotFound() : Results.Ok(AssetTypeFieldResponse.FromEntity(assetTypeField));
    }

    private static async Task<IResult> CreateAssetTypeFieldAsync(
        CreateAssetTypeFieldRequest request,
        IAssetTypeFieldRepository repository,
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
        var fieldId = $"{FieldIdPrefix}{nextSequence:D6}";

        var assetTypeField = await repository.CreateAsync(
            request.ToEntity(fieldId, asset.AssetName),
            cancellationToken);

        return Results.Created($"/api/asset-type-fields/{assetTypeField.Id}", AssetTypeFieldResponse.FromEntity(assetTypeField));
    }

    private static async Task<IResult> UpdateAssetTypeFieldAsync(
        string id,
        UpdateAssetTypeFieldRequest request,
        IAssetTypeFieldRepository repository,
        IAssetRepository assetRepository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset type field id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var assetTypeField = await repository.GetByIdAsync(id, cancellationToken);
        if (assetTypeField is null)
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

        request.ApplyTo(assetTypeField, asset.AssetName);

        var updated = await repository.UpdateAsync(id, assetTypeField, cancellationToken);
        return updated ? Results.Ok(AssetTypeFieldResponse.FromEntity(assetTypeField)) : Results.NotFound();
    }

    private static async Task<IResult> DeleteAssetTypeFieldAsync(
        string id,
        IAssetTypeFieldRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid asset type field id." });
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
