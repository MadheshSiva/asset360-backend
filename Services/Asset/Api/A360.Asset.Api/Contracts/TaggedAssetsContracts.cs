using TaggedAssetsEntity = A360.Asset.Domain.Entities.TaggedAssets;

namespace A360.Asset.Api.Contracts;

public sealed record CreateTaggedAssetsRequest(
    string? AssetId,
    string? AssetName,
    string? AssetCode,
    string? AssetDescription,
    string? Company,
    string? Site,
    string? Building,
    string? Floor,
    string? Room,
    string? MainCategory,
    string? SubCategory,
    string? SubSubCategory,
    string? Brand,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public TaggedAssetsEntity ToEntity(string taggedAssetId)
    {
        return new TaggedAssetsEntity
        {
            TaggedAssetId = taggedAssetId,
            AssetId = AssetId ?? string.Empty,
            AssetName = AssetName ?? string.Empty,
            AssetCode = AssetCode ?? string.Empty,
            AssetDescription = AssetDescription ?? string.Empty,
            Company = Company ?? string.Empty,
            Site = Site ?? string.Empty,
            Building = Building ?? string.Empty,
            Floor = Floor ?? string.Empty,
            Room = Room ?? string.Empty,
            MainCategory = MainCategory ?? string.Empty,
            SubCategory = SubCategory ?? string.Empty,
            SubSubCategory = SubSubCategory ?? string.Empty,
            Brand = Brand ?? string.Empty,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateTaggedAssetsRequest(
    string? AssetId,
    string? AssetName,
    string? AssetCode,
    string? AssetDescription,
    string? Company,
    string? Site,
    string? Building,
    string? Floor,
    string? Room,
    string? MainCategory,
    string? SubCategory,
    string? SubSubCategory,
    string? Brand,
    string? UpdatedBy)
{
    public void ApplyTo(TaggedAssetsEntity taggedAsset)
    {
        taggedAsset.AssetId = AssetId ?? string.Empty;
        taggedAsset.AssetName = AssetName ?? string.Empty;
        taggedAsset.AssetCode = AssetCode ?? string.Empty;
        taggedAsset.AssetDescription = AssetDescription ?? string.Empty;
        taggedAsset.Company = Company ?? string.Empty;
        taggedAsset.Site = Site ?? string.Empty;
        taggedAsset.Building = Building ?? string.Empty;
        taggedAsset.Floor = Floor ?? string.Empty;
        taggedAsset.Room = Room ?? string.Empty;
        taggedAsset.MainCategory = MainCategory ?? string.Empty;
        taggedAsset.SubCategory = SubCategory ?? string.Empty;
        taggedAsset.SubSubCategory = SubSubCategory ?? string.Empty;
        taggedAsset.Brand = Brand ?? string.Empty;
        taggedAsset.UpdatedBy = UpdatedBy;
        taggedAsset.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record TaggedAssetsResponse(
    string Id,
    string TaggedAssetId,
    string AssetId,
    string AssetName,
    string AssetCode,
    string AssetDescription,
    string Company,
    string Site,
    string Building,
    string Floor,
    string Room,
    string MainCategory,
    string SubCategory,
    string SubSubCategory,
    string Brand,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static TaggedAssetsResponse FromEntity(TaggedAssetsEntity taggedAsset)
    {
        return new TaggedAssetsResponse(
            taggedAsset.Id,
            taggedAsset.TaggedAssetId,
            taggedAsset.AssetId,
            taggedAsset.AssetName,
            taggedAsset.AssetCode,
            taggedAsset.AssetDescription,
            taggedAsset.Company,
            taggedAsset.Site,
            taggedAsset.Building,
            taggedAsset.Floor,
            taggedAsset.Room,
            taggedAsset.MainCategory,
            taggedAsset.SubCategory,
            taggedAsset.SubSubCategory,
            taggedAsset.Brand,
            taggedAsset.CreatedBy,
            taggedAsset.CreatedAt,
            taggedAsset.UpdatedBy,
            taggedAsset.UpdatedAt,
            taggedAsset.ClientId,
            taggedAsset.TenantId,
            taggedAsset.IsDeleted);
    }
}
