using CategoryEntity = A360.MasterManagement.Domain.Entities.Category;

namespace A360.MasterManagement.Api.Contracts;

public sealed record CreateCategoryRequest(
    string? AssetId,
    string? CategoryName,
    string? CategoryCode,
    string? Description,
    string? Level,
    string? Status,
    string? RelatedAsset,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public CategoryEntity ToEntity(string categoryId, string assetName)
    {
        return new CategoryEntity
        {
            CategoryId = categoryId,
            AssetId = AssetId ?? string.Empty,
            AssetName = assetName,
            CategoryName = CategoryName ?? string.Empty,
            CategoryCode = CategoryCode ?? string.Empty,
            Description = Description ?? string.Empty,
            Level = Level ?? string.Empty,
            Status = Status,
            RelatedAsset = RelatedAsset,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateCategoryRequest(
    string? AssetId,
    string? CategoryName,
    string? CategoryCode,
    string? Description,
    string? Level,
    string? Status,
    string? RelatedAsset,
    string? UpdatedBy)
{
    public void ApplyTo(CategoryEntity category, string assetName)
    {
        category.AssetId = AssetId ?? string.Empty;
        category.AssetName = assetName;
        category.CategoryName = CategoryName ?? string.Empty;
        category.CategoryCode = CategoryCode ?? string.Empty;
        category.Description = Description ?? string.Empty;
        category.Level = Level ?? string.Empty;
        category.Status = Status;
        category.RelatedAsset = RelatedAsset;
        category.UpdatedBy = UpdatedBy;
        category.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record CategoryResponse(
    string Id,
    string CategoryId,
    string AssetId,
    string AssetName,
    string CategoryName,
    string CategoryCode,
    string Description,
    string Level,
    string? Status,
    string? RelatedAsset,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static CategoryResponse FromEntity(CategoryEntity category)
    {
        return new CategoryResponse(
            category.Id,
            category.CategoryId,
            category.AssetId,
            category.AssetName,
            category.CategoryName,
            category.CategoryCode,
            category.Description,
            category.Level,
            category.Status,
            category.RelatedAsset,
            category.CreatedBy,
            category.CreatedAt,
            category.UpdatedBy,
            category.UpdatedAt,
            category.ClientId,
            category.TenantId,
            category.IsDeleted);
    }
}
