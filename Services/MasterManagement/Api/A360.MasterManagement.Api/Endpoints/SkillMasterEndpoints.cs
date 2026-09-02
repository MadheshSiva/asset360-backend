using A360.Asset.Repository.Repositories;
using A360.MasterManagement.Api.Contracts;
using A360.MasterManagement.Api.Validation;
using A360.MasterManagement.Repository.Repositories;
using A360.Repository.Repositories;
using A360.Repository.Sequences;

namespace A360.MasterManagement.Api.Endpoints;

public static class SkillMasterEndpoints
{
    private const string SequenceName = "skill_master";
    private const string SkillIdPrefix = "SKM";

    public static RouteGroupBuilder MapSkillMasterEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/skill-masters").WithTags("SkillMasters");

        group.MapGet("", GetSkillMastersAsync).WithName("GetSkillMasters");
        group.MapGet("/{id}", GetSkillMasterByIdAsync).WithName("GetSkillMasterById");
        group.MapPost("", CreateSkillMasterAsync).WithName("CreateSkillMaster");
        group.MapPut("/{id}", UpdateSkillMasterAsync).WithName("UpdateSkillMaster");
        group.MapDelete("/{id}", DeleteSkillMasterAsync).WithName("DeleteSkillMaster");

        return group;
    }

    private static async Task<IResult> GetSkillMastersAsync(
        ISkillMasterRepository repository,
        CancellationToken cancellationToken)
    {
        var skillMasters = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(skillMasters.Select(SkillMasterResponse.FromEntity));
    }

    private static async Task<IResult> GetSkillMasterByIdAsync(
        string id,
        ISkillMasterRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid skill master id." });
        }

        var skillMaster = await repository.GetByIdAsync(id, cancellationToken);
        return skillMaster is null ? Results.NotFound() : Results.Ok(SkillMasterResponse.FromEntity(skillMaster));
    }

    private static async Task<IResult> CreateSkillMasterAsync(
        CreateSkillMasterRequest request,
        ISkillMasterRepository repository,
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
        var skillId = $"{SkillIdPrefix}{nextSequence:D6}";

        var skillMaster = await repository.CreateAsync(
            request.ToEntity(skillId, asset.AssetName),
            cancellationToken);

        return Results.Created($"/api/skill-masters/{skillMaster.Id}", SkillMasterResponse.FromEntity(skillMaster));
    }

    private static async Task<IResult> UpdateSkillMasterAsync(
        string id,
        UpdateSkillMasterRequest request,
        ISkillMasterRepository repository,
        IAssetRepository assetRepository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid skill master id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var skillMaster = await repository.GetByIdAsync(id, cancellationToken);
        if (skillMaster is null)
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

        request.ApplyTo(skillMaster, asset.AssetName);

        var updated = await repository.UpdateAsync(id, skillMaster, cancellationToken);
        return updated ? Results.Ok(SkillMasterResponse.FromEntity(skillMaster)) : Results.NotFound();
    }

    private static async Task<IResult> DeleteSkillMasterAsync(
        string id,
        ISkillMasterRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid skill master id." });
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
