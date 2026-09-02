using A360.Asset.Repository.Repositories;
using A360.MasterManagement.Api.Contracts;
using A360.MasterManagement.Api.Validation;
using A360.MasterManagement.Repository.Repositories;
using A360.Repository.Repositories;
using A360.Repository.Sequences;

namespace A360.MasterManagement.Api.Endpoints;

public static class ChecklistTypeMasterEndpoints
{
    private const string SequenceName = "checklist_type_master";
    private const string TypeIdPrefix = "CTL";

    public static RouteGroupBuilder MapChecklistTypeMasterEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/checklist-type-masters").WithTags("ChecklistTypeMasters");

        group.MapGet("", GetChecklistTypeMastersAsync).WithName("GetChecklistTypeMasters");
        group.MapGet("/{id}", GetChecklistTypeMasterByIdAsync).WithName("GetChecklistTypeMasterById");
        group.MapPost("", CreateChecklistTypeMasterAsync).WithName("CreateChecklistTypeMaster");
        group.MapPut("/{id}", UpdateChecklistTypeMasterAsync).WithName("UpdateChecklistTypeMaster");
        group.MapDelete("/{id}", DeleteChecklistTypeMasterAsync).WithName("DeleteChecklistTypeMaster");

        return group;
    }

    private static async Task<IResult> GetChecklistTypeMastersAsync(
        IChecklistTypeMasterRepository repository,
        CancellationToken cancellationToken)
    {
        var checklistTypeMasters = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(checklistTypeMasters.Select(ChecklistTypeMasterResponse.FromEntity));
    }

    private static async Task<IResult> GetChecklistTypeMasterByIdAsync(
        string id,
        IChecklistTypeMasterRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid checklist type master id." });
        }

        var checklistTypeMaster = await repository.GetByIdAsync(id, cancellationToken);
        return checklistTypeMaster is null ? Results.NotFound() : Results.Ok(ChecklistTypeMasterResponse.FromEntity(checklistTypeMaster));
    }

    private static async Task<IResult> CreateChecklistTypeMasterAsync(
        CreateChecklistTypeMasterRequest request,
        IChecklistTypeMasterRepository repository,
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
        var typeId = $"{TypeIdPrefix}{nextSequence:D6}";

        var checklistTypeMaster = await repository.CreateAsync(
            request.ToEntity(typeId, asset.AssetName),
            cancellationToken);

        return Results.Created($"/api/checklist-type-masters/{checklistTypeMaster.Id}", ChecklistTypeMasterResponse.FromEntity(checklistTypeMaster));
    }

    private static async Task<IResult> UpdateChecklistTypeMasterAsync(
        string id,
        UpdateChecklistTypeMasterRequest request,
        IChecklistTypeMasterRepository repository,
        IAssetRepository assetRepository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid checklist type master id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var checklistTypeMaster = await repository.GetByIdAsync(id, cancellationToken);
        if (checklistTypeMaster is null)
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

        request.ApplyTo(checklistTypeMaster, asset.AssetName);

        var updated = await repository.UpdateAsync(id, checklistTypeMaster, cancellationToken);
        return updated ? Results.Ok(ChecklistTypeMasterResponse.FromEntity(checklistTypeMaster)) : Results.NotFound();
    }

    private static async Task<IResult> DeleteChecklistTypeMasterAsync(
        string id,
        IChecklistTypeMasterRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid checklist type master id." });
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
