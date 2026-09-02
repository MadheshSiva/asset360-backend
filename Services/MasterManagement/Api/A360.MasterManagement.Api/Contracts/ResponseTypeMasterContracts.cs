using ResponseTypeMasterEntity = A360.MasterManagement.Domain.Entities.ResponseTypeMaster;

namespace A360.MasterManagement.Api.Contracts;

public sealed record CreateResponseTypeMasterRequest(
    string? AssetId,
    string? TypeName,
    string? ValidationType,
    bool IsActive,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public ResponseTypeMasterEntity ToEntity(string typeId, string assetName)
    {
        return new ResponseTypeMasterEntity
        {
            TypeId = typeId,
            AssetId = AssetId ?? string.Empty,
            AssetName = assetName,
            TypeName = TypeName ?? string.Empty,
            ValidationType = ValidationType ?? string.Empty,
            IsActive = IsActive,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateResponseTypeMasterRequest(
    string? AssetId,
    string? TypeName,
    string? ValidationType,
    bool IsActive,
    string? UpdatedBy)
{
    public void ApplyTo(ResponseTypeMasterEntity responseTypeMaster, string assetName)
    {
        responseTypeMaster.AssetId = AssetId ?? string.Empty;
        responseTypeMaster.AssetName = assetName;
        responseTypeMaster.TypeName = TypeName ?? string.Empty;
        responseTypeMaster.ValidationType = ValidationType ?? string.Empty;
        responseTypeMaster.IsActive = IsActive;
        responseTypeMaster.UpdatedBy = UpdatedBy;
        responseTypeMaster.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record ResponseTypeMasterResponse(
    string Id,
    string TypeId,
    string AssetId,
    string AssetName,
    string TypeName,
    string ValidationType,
    bool IsActive,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static ResponseTypeMasterResponse FromEntity(ResponseTypeMasterEntity responseTypeMaster)
    {
        return new ResponseTypeMasterResponse(
            responseTypeMaster.Id,
            responseTypeMaster.TypeId,
            responseTypeMaster.AssetId,
            responseTypeMaster.AssetName,
            responseTypeMaster.TypeName,
            responseTypeMaster.ValidationType,
            responseTypeMaster.IsActive,
            responseTypeMaster.CreatedBy,
            responseTypeMaster.CreatedAt,
            responseTypeMaster.UpdatedBy,
            responseTypeMaster.UpdatedAt,
            responseTypeMaster.ClientId,
            responseTypeMaster.TenantId,
            responseTypeMaster.IsDeleted);
    }
}
