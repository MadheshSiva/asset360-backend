using A360.Asset.Repository.Repositories;
using A360.MasterManagement.Api.Contracts;
using A360.MasterManagement.Api.Validation;
using A360.MasterManagement.Repository.Repositories;
using A360.Repository.Repositories;
using A360.Repository.Sequences;

namespace A360.MasterManagement.Api.Endpoints;

public static class CategoryEndpoints
{
    private const string SequenceName = "category";
    private const string CategoryIdPrefix = "CAT";

    public static RouteGroupBuilder MapCategoryEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/categories").WithTags("Categories");

        group.MapGet("", GetCategoriesAsync).WithName("GetCategories");
        group.MapGet("/{id}", GetCategoryByIdAsync).WithName("GetCategoryById");
        group.MapPost("", CreateCategoryAsync).WithName("CreateCategory");
        group.MapPut("/{id}", UpdateCategoryAsync).WithName("UpdateCategory");
        group.MapDelete("/{id}", DeleteCategoryAsync).WithName("DeleteCategory");

        return group;
    }

    private static async Task<IResult> GetCategoriesAsync(
        ICategoryRepository repository,
        CancellationToken cancellationToken)
    {
        var categories = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(categories.Select(CategoryResponse.FromEntity));
    }

    private static async Task<IResult> GetCategoryByIdAsync(
        string id,
        ICategoryRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid category id." });
        }

        var category = await repository.GetByIdAsync(id, cancellationToken);
        return category is null ? Results.NotFound() : Results.Ok(CategoryResponse.FromEntity(category));
    }

    private static async Task<IResult> CreateCategoryAsync(
        CreateCategoryRequest request,
        ICategoryRepository repository,
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
        var categoryId = $"{CategoryIdPrefix}{nextSequence:D6}";

        var category = await repository.CreateAsync(
            request.ToEntity(categoryId, asset.AssetName),
            cancellationToken);

        return Results.Created($"/api/categories/{category.Id}", CategoryResponse.FromEntity(category));
    }

    private static async Task<IResult> UpdateCategoryAsync(
        string id,
        UpdateCategoryRequest request,
        ICategoryRepository repository,
        IAssetRepository assetRepository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid category id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        var category = await repository.GetByIdAsync(id, cancellationToken);
        if (category is null)
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

        request.ApplyTo(category, asset.AssetName);

        var updated = await repository.UpdateAsync(id, category, cancellationToken);
        return updated ? Results.Ok(CategoryResponse.FromEntity(category)) : Results.NotFound();
    }

    private static async Task<IResult> DeleteCategoryAsync(
        string id,
        ICategoryRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid category id." });
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
