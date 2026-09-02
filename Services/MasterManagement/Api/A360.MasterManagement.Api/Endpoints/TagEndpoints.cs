using A360.Asset.Repository.Repositories;
using A360.MasterManagement.Api.Contracts;
using A360.MasterManagement.Api.Validation;
using A360.MasterManagement.Repository.Repositories;
using A360.Repository.Repositories;
using A360.Repository.Sequences;

namespace A360.MasterManagement.Api.Endpoints;

public static class TagEndpoints
{
    private const string SequenceName = "tag";
    private const string TagIdPrefix = "TAG";

    public static RouteGroupBuilder MapTagEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/tags").WithTags("Tags");

        group.MapGet("", GetTagsAsync).WithName("GetTags");
        group.MapGet("/{id}", GetTagByIdAsync).WithName("GetTagById");
        group.MapPost("", CreateTagAsync).WithName("CreateTag");
        group.MapPut("/{id}", UpdateTagAsync).WithName("UpdateTag");
        group.MapDelete("/{id}", DeleteTagAsync).WithName("DeleteTag");

        return group;
    }

    private static async Task<IResult> GetTagsAsync(
        ITagRepository repository,
        CancellationToken cancellationToken)
    {
        var tags = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(tags.Select(TagResponse.FromEntity));
    }

    private static async Task<IResult> GetTagByIdAsync(
        string id,
        ITagRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid tag id." });
        }

        var tag = await repository.GetByIdAsync(id, cancellationToken);
        return tag is null ? Results.NotFound() : Results.Ok(TagResponse.FromEntity(tag));
    }

    private static async Task<IResult> CreateTagAsync(
        CreateTagRequest request,
        ITagRepository repository,
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
        var tagId = $"{TagIdPrefix}{nextSequence:D6}";

        var tag = await repository.CreateAsync(
            request.ToEntity(tagId, asset.AssetName),
            cancellationToken);

        return Results.Created($"/api/tags/{tag.Id}", TagResponse.FromEntity(tag));
    }

    private static async Task<IResult> UpdateTagAsync(
        string id,
        UpdateTagRequest request,
        ITagRepository repository,
        IAssetRepository assetRepository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid tag id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var tag = await repository.GetByIdAsync(id, cancellationToken);
        if (tag is null)
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

        request.ApplyTo(tag, asset.AssetName);

        var updated = await repository.UpdateAsync(id, tag, cancellationToken);
        return updated ? Results.Ok(TagResponse.FromEntity(tag)) : Results.NotFound();
    }

    private static async Task<IResult> DeleteTagAsync(
        string id,
        ITagRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid tag id." });
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
