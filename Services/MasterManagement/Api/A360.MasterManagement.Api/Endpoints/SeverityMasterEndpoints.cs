using A360.Asset.Repository.Repositories;
using A360.MasterManagement.Api.Contracts;
using A360.MasterManagement.Api.Validation;
using A360.MasterManagement.Repository.Repositories;
using A360.Repository.Repositories;
using A360.Repository.Sequences;

namespace A360.MasterManagement.Api.Endpoints;

public static class SeverityMasterEndpoints
{
    private const string SequenceName = "severity_master";
    private const string SeverityIdPrefix = "SVM";

    public static RouteGroupBuilder MapSeverityMasterEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/severity-masters").WithTags("SeverityMasters");

        group.MapGet("", GetSeverityMastersAsync).WithName("GetSeverityMasters");
        group.MapGet("/{id}", GetSeverityMasterByIdAsync).WithName("GetSeverityMasterById");
        group.MapPost("", CreateSeverityMasterAsync).WithName("CreateSeverityMaster");
        group.MapPut("/{id}", UpdateSeverityMasterAsync).WithName("UpdateSeverityMaster");
        group.MapDelete("/{id}", DeleteSeverityMasterAsync).WithName("DeleteSeverityMaster");

        return group;
    }

    private static async Task<IResult> GetSeverityMastersAsync(
        ISeverityMasterRepository repository,
        CancellationToken cancellationToken)
    {
        var severityMasters = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(severityMasters.Select(SeverityMasterResponse.FromEntity));
    }

    private static async Task<IResult> GetSeverityMasterByIdAsync(
        string id,
        ISeverityMasterRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid severity id." });
        }

        var severityMaster = await repository.GetByIdAsync(id, cancellationToken);
        return severityMaster is null ? Results.NotFound() : Results.Ok(SeverityMasterResponse.FromEntity(severityMaster));
    }

    private static async Task<IResult> CreateSeverityMasterAsync(
        CreateSeverityMasterRequest request,
        ISeverityMasterRepository repository,
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
        var severityId = $"{SeverityIdPrefix}{nextSequence:D6}";

        var severityMaster = await repository.CreateAsync(
            request.ToEntity(severityId, asset.AssetName),
            cancellationToken);

        return Results.Created($"/api/severity-masters/{severityMaster.Id}", SeverityMasterResponse.FromEntity(severityMaster));
    }

    private static async Task<IResult> UpdateSeverityMasterAsync(
        string id,
        UpdateSeverityMasterRequest request,
        ISeverityMasterRepository repository,
        IAssetRepository assetRepository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid severity id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var severityMaster = await repository.GetByIdAsync(id, cancellationToken);
        if (severityMaster is null)
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

        request.ApplyTo(severityMaster, asset.AssetName);

        var updated = await repository.UpdateAsync(id, severityMaster, cancellationToken);
        return updated ? Results.Ok(SeverityMasterResponse.FromEntity(severityMaster)) : Results.NotFound();
    }

    private static async Task<IResult> DeleteSeverityMasterAsync(
        string id,
        ISeverityMasterRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid severity id." });
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
