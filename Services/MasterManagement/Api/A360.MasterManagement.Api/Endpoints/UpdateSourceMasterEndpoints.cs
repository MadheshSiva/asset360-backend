using A360.Asset.Repository.Repositories;
using A360.MasterManagement.Api.Contracts;
using A360.MasterManagement.Api.Validation;
using A360.MasterManagement.Repository.Repositories;
using A360.Repository.Repositories;
using A360.Repository.Sequences;

namespace A360.MasterManagement.Api.Endpoints;

public static class UpdateSourceMasterEndpoints
{
    private const string SequenceName = "update_source_master";
    private const string UpdateSourceMasterIdPrefix = "USM";

    public static RouteGroupBuilder MapUpdateSourceMasterEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/update-source-masters").WithTags("UpdateSourceMasters");

        group.MapGet("", GetUpdateSourceMastersAsync).WithName("GetUpdateSourceMasters");
        group.MapGet("/{id}", GetUpdateSourceMasterByIdAsync).WithName("GetUpdateSourceMasterById");
        group.MapPost("", CreateUpdateSourceMasterAsync).WithName("CreateUpdateSourceMaster");
        group.MapPut("/{id}", UpdateUpdateSourceMasterAsync).WithName("UpdateUpdateSourceMaster");
        group.MapDelete("/{id}", DeleteUpdateSourceMasterAsync).WithName("DeleteUpdateSourceMaster");

        return group;
    }

    private static async Task<IResult> GetUpdateSourceMastersAsync(
        IUpdateSourceMasterRepository repository,
        CancellationToken cancellationToken)
    {
        var updateSourceMasters = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(updateSourceMasters.Select(UpdateSourceMasterResponse.FromEntity));
    }

    private static async Task<IResult> GetUpdateSourceMasterByIdAsync(
        string id,
        IUpdateSourceMasterRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid update source master id." });
        }

        var updateSourceMaster = await repository.GetByIdAsync(id, cancellationToken);
        return updateSourceMaster is null ? Results.NotFound() : Results.Ok(UpdateSourceMasterResponse.FromEntity(updateSourceMaster));
    }

    private static async Task<IResult> CreateUpdateSourceMasterAsync(
        CreateUpdateSourceMasterRequest request,
        IUpdateSourceMasterRepository repository,
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
        var sourceId = $"{UpdateSourceMasterIdPrefix}{nextSequence:D6}";

        var updateSourceMaster = await repository.CreateAsync(
            request.ToEntity(sourceId, asset.AssetName),
            cancellationToken);

        return Results.Created($"/api/update-source-masters/{updateSourceMaster.Id}", UpdateSourceMasterResponse.FromEntity(updateSourceMaster));
    }

    private static async Task<IResult> UpdateUpdateSourceMasterAsync(
        string id,
        UpdateUpdateSourceMasterRequest request,
        IUpdateSourceMasterRepository repository,
        IAssetRepository assetRepository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid update source master id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var updateSourceMaster = await repository.GetByIdAsync(id, cancellationToken);
        if (updateSourceMaster is null)
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

        request.ApplyTo(updateSourceMaster, asset.AssetName);

        var updated = await repository.UpdateAsync(id, updateSourceMaster, cancellationToken);
        return updated ? Results.Ok(UpdateSourceMasterResponse.FromEntity(updateSourceMaster)) : Results.NotFound();
    }

    private static async Task<IResult> DeleteUpdateSourceMasterAsync(
        string id,
        IUpdateSourceMasterRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid update source master id." });
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
