using AssetDomainEntity = A360.Asset.Domain.Entities.AssetDomain;

namespace A360.Asset.Api.Contracts;

public sealed record CreateAssetDomainRequest(
    string? AssetId,
    string? AssetName,
    string? AssetType,
    string? FieldName,
    string? FieldValue,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public AssetDomainEntity ToEntity(string assetDomainId)
    {
        return new AssetDomainEntity
        {
            AssetDomainId = assetDomainId,
            AssetId = AssetId ?? string.Empty,
            AssetName = AssetName ?? string.Empty,
            AssetType = AssetType ?? string.Empty,
            FieldName = FieldName ?? string.Empty,
            FieldValue = FieldValue ?? string.Empty,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateAssetDomainRequest(
    string? AssetId,
    string? AssetName,
    string? AssetType,
    string? FieldName,
    string? FieldValue,
    string? UpdatedBy)
{
    public void ApplyTo(AssetDomainEntity assetDomain)
    {
        assetDomain.AssetId = AssetId ?? string.Empty;
        assetDomain.AssetName = AssetName ?? string.Empty;
        assetDomain.AssetType = AssetType ?? string.Empty;
        assetDomain.FieldName = FieldName ?? string.Empty;
        assetDomain.FieldValue = FieldValue ?? string.Empty;
        assetDomain.UpdatedBy = UpdatedBy;
        assetDomain.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record AssetDomainResponse(
    string Id,
    string AssetDomainId,
    string AssetId,
    string AssetName,
    string AssetType,
    string FieldName,
    string FieldValue,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static AssetDomainResponse FromEntity(AssetDomainEntity assetDomain)
    {
        return new AssetDomainResponse(
            assetDomain.Id,
            assetDomain.AssetDomainId,
            assetDomain.AssetId,
            assetDomain.AssetName,
            assetDomain.AssetType,
            assetDomain.FieldName,
            assetDomain.FieldValue,
            assetDomain.CreatedBy,
            assetDomain.CreatedAt,
            assetDomain.UpdatedBy,
            assetDomain.UpdatedAt,
            assetDomain.ClientId,
            assetDomain.TenantId,
            assetDomain.IsDeleted);
    }
}
