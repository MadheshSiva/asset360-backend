using AssetEntity = A360.Asset.Domain.Entities.Asset;

namespace A360.Asset.Api.Contracts;

public sealed record CreateAssetRequest(
    string? AssetName,
    string? Description,
    string? CategorySubCategory,
    string? SerialNumber,
    string? TagIds,
    string? AssetType,
    string? ParentAsset,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public AssetEntity ToEntity(string assetId)
    {
        return new AssetEntity
        {
            AssetId = assetId,
            AssetName = AssetName ?? string.Empty,
            Description = Description ?? string.Empty,
            CategorySubCategory = CategorySubCategory ?? string.Empty,
            SerialNumber = SerialNumber ?? string.Empty,
            TagIds = TagIds ?? string.Empty,
            AssetType = AssetType ?? string.Empty,
            ParentAsset = ParentAsset,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateAssetRequest(
    string? AssetName,
    string? Description,
    string? CategorySubCategory,
    string? SerialNumber,
    string? TagIds,
    string? AssetType,
    string? ParentAsset,
    string? UpdatedBy)
{
    public void ApplyTo(AssetEntity asset)
    {
        asset.AssetName = AssetName ?? string.Empty;
        asset.Description = Description ?? string.Empty;
        asset.CategorySubCategory = CategorySubCategory ?? string.Empty;
        asset.SerialNumber = SerialNumber ?? string.Empty;
        asset.TagIds = TagIds ?? string.Empty;
        asset.AssetType = AssetType ?? string.Empty;
        asset.ParentAsset = ParentAsset;
        asset.UpdatedBy = UpdatedBy;
        asset.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record AssetResponse(
    string Id,
    string AssetId,
    string AssetName,
    string Description,
    string CategorySubCategory,
    string SerialNumber,
    string TagIds,
    string AssetType,
    string? ParentAsset,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static AssetResponse FromEntity(AssetEntity asset)
    {
        return new AssetResponse(
            asset.Id,
            asset.AssetId,
            asset.AssetName,
            asset.Description,
            asset.CategorySubCategory,
            asset.SerialNumber,
            asset.TagIds,
            asset.AssetType,
            asset.ParentAsset,
            asset.CreatedBy,
            asset.CreatedAt,
            asset.UpdatedBy,
            asset.UpdatedAt,
            asset.ClientId,
            asset.TenantId,
            asset.IsDeleted);
    }
}
