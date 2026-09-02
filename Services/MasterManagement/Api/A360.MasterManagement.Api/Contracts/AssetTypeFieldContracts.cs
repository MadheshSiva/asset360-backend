using AssetTypeFieldEntity = A360.MasterManagement.Domain.Entities.AssetTypeField;

namespace A360.MasterManagement.Api.Contracts;

public sealed record CreateAssetTypeFieldRequest(
    string? AssetId,
    string? AssetType,
    string? FieldName,
    string? FieldType,
    bool IsRequired,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public AssetTypeFieldEntity ToEntity(string fieldId, string assetName)
    {
        return new AssetTypeFieldEntity
        {
            FieldId = fieldId,
            AssetId = AssetId ?? string.Empty,
            AssetName = assetName,
            AssetType = AssetType ?? string.Empty,
            FieldName = FieldName ?? string.Empty,
            FieldType = FieldType ?? string.Empty,
            IsRequired = IsRequired,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateAssetTypeFieldRequest(
    string? AssetId,
    string? AssetType,
    string? FieldName,
    string? FieldType,
    bool IsRequired,
    string? UpdatedBy)
{
    public void ApplyTo(AssetTypeFieldEntity assetTypeField, string assetName)
    {
        assetTypeField.AssetId = AssetId ?? string.Empty;
        assetTypeField.AssetName = assetName;
        assetTypeField.AssetType = AssetType ?? string.Empty;
        assetTypeField.FieldName = FieldName ?? string.Empty;
        assetTypeField.FieldType = FieldType ?? string.Empty;
        assetTypeField.IsRequired = IsRequired;
        assetTypeField.UpdatedBy = UpdatedBy;
        assetTypeField.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record AssetTypeFieldResponse(
    string Id,
    string FieldId,
    string AssetId,
    string AssetName,
    string AssetType,
    string FieldName,
    string FieldType,
    bool IsRequired,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static AssetTypeFieldResponse FromEntity(AssetTypeFieldEntity assetTypeField)
    {
        return new AssetTypeFieldResponse(
            assetTypeField.Id,
            assetTypeField.FieldId,
            assetTypeField.AssetId,
            assetTypeField.AssetName,
            assetTypeField.AssetType,
            assetTypeField.FieldName,
            assetTypeField.FieldType,
            assetTypeField.IsRequired,
            assetTypeField.CreatedBy,
            assetTypeField.CreatedAt,
            assetTypeField.UpdatedBy,
            assetTypeField.UpdatedAt,
            assetTypeField.ClientId,
            assetTypeField.TenantId,
            assetTypeField.IsDeleted);
    }
}
