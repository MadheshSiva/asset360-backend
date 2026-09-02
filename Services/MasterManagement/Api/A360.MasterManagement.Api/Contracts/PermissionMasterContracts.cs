using PermissionMasterEntity = A360.MasterManagement.Domain.Entities.PermissionMaster;

namespace A360.MasterManagement.Api.Contracts;

public sealed record CreatePermissionMasterRequest(
    string? AssetId,
    string? PermissionName,
    string? Module,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public PermissionMasterEntity ToEntity(string permissionId, string assetName)
    {
        return new PermissionMasterEntity
        {
            PermissionId = permissionId,
            AssetId = AssetId ?? string.Empty,
            AssetName = assetName,
            PermissionName = PermissionName ?? string.Empty,
            Module = Module ?? string.Empty,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdatePermissionMasterRequest(
    string? AssetId,
    string? PermissionName,
    string? Module,
    string? UpdatedBy)
{
    public void ApplyTo(PermissionMasterEntity permissionMaster, string assetName)
    {
        permissionMaster.AssetId = AssetId ?? string.Empty;
        permissionMaster.AssetName = assetName;
        permissionMaster.PermissionName = PermissionName ?? string.Empty;
        permissionMaster.Module = Module ?? string.Empty;
        permissionMaster.UpdatedBy = UpdatedBy;
        permissionMaster.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record PermissionMasterResponse(
    string Id,
    string PermissionId,
    string AssetId,
    string AssetName,
    string PermissionName,
    string Module,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static PermissionMasterResponse FromEntity(PermissionMasterEntity permissionMaster)
    {
        return new PermissionMasterResponse(
            permissionMaster.Id,
            permissionMaster.PermissionId,
            permissionMaster.AssetId,
            permissionMaster.AssetName,
            permissionMaster.PermissionName,
            permissionMaster.Module,
            permissionMaster.CreatedBy,
            permissionMaster.CreatedAt,
            permissionMaster.UpdatedBy,
            permissionMaster.UpdatedAt,
            permissionMaster.ClientId,
            permissionMaster.TenantId,
            permissionMaster.IsDeleted);
    }
}
