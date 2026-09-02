using AssetTypeEntity = A360.MasterManagement.Domain.Entities.AssetType;

namespace A360.MasterManagement.Api.Contracts;

public sealed record CreateAssetTypeRequest(
    string? AssetId,
    string? AssetTypeName,
    string? AssetTypeCode,
    string? Description,
    string? Status,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public AssetTypeEntity ToEntity(string assetTypeId, string assetName)
    {
        return new AssetTypeEntity
        {
            AssetTypeId = assetTypeId,
            AssetId = AssetId ?? string.Empty,
            AssetName = assetName,
            AssetTypeName = AssetTypeName ?? string.Empty,
            AssetTypeCode = AssetTypeCode ?? string.Empty,
            Description = Description ?? string.Empty,
            Status = Status,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateAssetTypeRequest(
    string? AssetId,
    string? AssetTypeName,
    string? AssetTypeCode,
    string? Description,
    string? Status,
    string? UpdatedBy)
{
    public void ApplyTo(AssetTypeEntity assetType, string assetName)
    {
        assetType.AssetId = AssetId ?? string.Empty;
        assetType.AssetName = assetName;
        assetType.AssetTypeName = AssetTypeName ?? string.Empty;
        assetType.AssetTypeCode = AssetTypeCode ?? string.Empty;
        assetType.Description = Description ?? string.Empty;
        assetType.Status = Status;
        assetType.UpdatedBy = UpdatedBy;
        assetType.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record AssetTypeResponse(
    string Id,
    string AssetTypeId,
    string AssetId,
    string AssetName,
    string AssetTypeName,
    string AssetTypeCode,
    string Description,
    string? Status,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static AssetTypeResponse FromEntity(AssetTypeEntity assetType)
    {
        return new AssetTypeResponse(
            assetType.Id,
            assetType.AssetTypeId,
            assetType.AssetId,
            assetType.AssetName,
            assetType.AssetTypeName,
            assetType.AssetTypeCode,
            assetType.Description,
            assetType.Status,
            assetType.CreatedBy,
            assetType.CreatedAt,
            assetType.UpdatedBy,
            assetType.UpdatedAt,
            assetType.ClientId,
            assetType.TenantId,
            assetType.IsDeleted);
    }
}
