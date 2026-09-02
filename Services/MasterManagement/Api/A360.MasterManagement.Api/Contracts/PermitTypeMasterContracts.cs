using PermitTypeMasterEntity = A360.MasterManagement.Domain.Entities.PermitTypeMaster;

namespace A360.MasterManagement.Api.Contracts;

public sealed record CreatePermitTypeMasterRequest(
    string? AssetId,
    string? PermitName,
    int ValidityDays,
    bool IsApprovalRequired,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public PermitTypeMasterEntity ToEntity(string permitTypeId, string assetName)
    {
        return new PermitTypeMasterEntity
        {
            PermitTypeId = permitTypeId,
            AssetId = AssetId ?? string.Empty,
            AssetName = assetName,
            PermitName = PermitName ?? string.Empty,
            ValidityDays = ValidityDays,
            IsApprovalRequired = IsApprovalRequired,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdatePermitTypeMasterRequest(
    string? AssetId,
    string? PermitName,
    int ValidityDays,
    bool IsApprovalRequired,
    string? UpdatedBy)
{
    public void ApplyTo(PermitTypeMasterEntity permitTypeMaster, string assetName)
    {
        permitTypeMaster.AssetId = AssetId ?? string.Empty;
        permitTypeMaster.AssetName = assetName;
        permitTypeMaster.PermitName = PermitName ?? string.Empty;
        permitTypeMaster.ValidityDays = ValidityDays;
        permitTypeMaster.IsApprovalRequired = IsApprovalRequired;
        permitTypeMaster.UpdatedBy = UpdatedBy;
        permitTypeMaster.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record PermitTypeMasterResponse(
    string Id,
    string PermitTypeId,
    string AssetId,
    string AssetName,
    string PermitName,
    int ValidityDays,
    bool IsApprovalRequired,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static PermitTypeMasterResponse FromEntity(PermitTypeMasterEntity permitTypeMaster)
    {
        return new PermitTypeMasterResponse(
            permitTypeMaster.Id,
            permitTypeMaster.PermitTypeId,
            permitTypeMaster.AssetId,
            permitTypeMaster.AssetName,
            permitTypeMaster.PermitName,
            permitTypeMaster.ValidityDays,
            permitTypeMaster.IsApprovalRequired,
            permitTypeMaster.CreatedBy,
            permitTypeMaster.CreatedAt,
            permitTypeMaster.UpdatedBy,
            permitTypeMaster.UpdatedAt,
            permitTypeMaster.ClientId,
            permitTypeMaster.TenantId,
            permitTypeMaster.IsDeleted);
    }
}
