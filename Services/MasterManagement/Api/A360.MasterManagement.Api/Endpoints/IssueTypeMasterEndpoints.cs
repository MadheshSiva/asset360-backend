using A360.Asset.Repository.Repositories;
using A360.MasterManagement.Api.Contracts;
using A360.MasterManagement.Api.Validation;
using A360.MasterManagement.Repository.Repositories;
using A360.Repository.Repositories;
using A360.Repository.Sequences;

namespace A360.MasterManagement.Api.Endpoints;

public static class IssueTypeMasterEndpoints
{
    private const string SequenceName = "issue_type_master";
    private const string IssueTypeIdPrefix = "ITM";

    public static RouteGroupBuilder MapIssueTypeMasterEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/issue-type-masters").WithTags("IssueTypeMasters");

        group.MapGet("", GetIssueTypeMastersAsync).WithName("GetIssueTypeMasters");
        group.MapGet("/{id}", GetIssueTypeMasterByIdAsync).WithName("GetIssueTypeMasterById");
        group.MapPost("", CreateIssueTypeMasterAsync).WithName("CreateIssueTypeMaster");
        group.MapPut("/{id}", UpdateIssueTypeMasterAsync).WithName("UpdateIssueTypeMaster");
        group.MapDelete("/{id}", DeleteIssueTypeMasterAsync).WithName("DeleteIssueTypeMaster");

        return group;
    }

    private static async Task<IResult> GetIssueTypeMastersAsync(
        IIssueTypeMasterRepository repository,
        CancellationToken cancellationToken)
    {
        var issueTypeMasters = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(issueTypeMasters.Select(IssueTypeMasterResponse.FromEntity));
    }

    private static async Task<IResult> GetIssueTypeMasterByIdAsync(
        string id,
        IIssueTypeMasterRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid issue type id." });
        }

        var issueTypeMaster = await repository.GetByIdAsync(id, cancellationToken);
        return issueTypeMaster is null ? Results.NotFound() : Results.Ok(IssueTypeMasterResponse.FromEntity(issueTypeMaster));
    }

    private static async Task<IResult> CreateIssueTypeMasterAsync(
        CreateIssueTypeMasterRequest request,
        IIssueTypeMasterRepository repository,
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
        var issueTypeId = $"{IssueTypeIdPrefix}{nextSequence:D6}";

        var issueTypeMaster = await repository.CreateAsync(
            request.ToEntity(issueTypeId, asset.AssetName),
            cancellationToken);

        return Results.Created($"/api/issue-type-masters/{issueTypeMaster.Id}", IssueTypeMasterResponse.FromEntity(issueTypeMaster));
    }

    private static async Task<IResult> UpdateIssueTypeMasterAsync(
        string id,
        UpdateIssueTypeMasterRequest request,
        IIssueTypeMasterRepository repository,
        IAssetRepository assetRepository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid issue type id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var issueTypeMaster = await repository.GetByIdAsync(id, cancellationToken);
        if (issueTypeMaster is null)
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

        request.ApplyTo(issueTypeMaster, asset.AssetName);

        var updated = await repository.UpdateAsync(id, issueTypeMaster, cancellationToken);
        return updated ? Results.Ok(IssueTypeMasterResponse.FromEntity(issueTypeMaster)) : Results.NotFound();
    }

    private static async Task<IResult> DeleteIssueTypeMasterAsync(
        string id,
        IIssueTypeMasterRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid issue type id." });
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
