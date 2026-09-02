using ModuleAccessMasterEntity = A360.MasterManagement.Domain.Entities.ModuleAccessMaster;

namespace A360.MasterManagement.Api.Contracts;

public sealed record CreateModuleAccessMasterRequest(
    string? AssetId,
    string? ModuleName,
    string? RoutePath,
    string? Icon,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public ModuleAccessMasterEntity ToEntity(string moduleId, string assetName)
    {
        return new ModuleAccessMasterEntity
        {
            ModuleId = moduleId,
            AssetId = AssetId ?? string.Empty,
            AssetName = assetName,
            ModuleName = ModuleName ?? string.Empty,
            RoutePath = RoutePath ?? string.Empty,
            Icon = Icon ?? string.Empty,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateModuleAccessMasterRequest(
    string? AssetId,
    string? ModuleName,
    string? RoutePath,
    string? Icon,
    string? UpdatedBy)
{
    public void ApplyTo(ModuleAccessMasterEntity moduleAccessMaster, string assetName)
    {
        moduleAccessMaster.AssetId = AssetId ?? string.Empty;
        moduleAccessMaster.AssetName = assetName;
        moduleAccessMaster.ModuleName = ModuleName ?? string.Empty;
        moduleAccessMaster.RoutePath = RoutePath ?? string.Empty;
        moduleAccessMaster.Icon = Icon ?? string.Empty;
        moduleAccessMaster.UpdatedBy = UpdatedBy;
        moduleAccessMaster.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record ModuleAccessMasterResponse(
    string Id,
    string ModuleId,
    string AssetId,
    string AssetName,
    string ModuleName,
    string RoutePath,
    string Icon,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static ModuleAccessMasterResponse FromEntity(ModuleAccessMasterEntity moduleAccessMaster)
    {
        return new ModuleAccessMasterResponse(
            moduleAccessMaster.Id,
            moduleAccessMaster.ModuleId,
            moduleAccessMaster.AssetId,
            moduleAccessMaster.AssetName,
            moduleAccessMaster.ModuleName,
            moduleAccessMaster.RoutePath,
            moduleAccessMaster.Icon,
            moduleAccessMaster.CreatedBy,
            moduleAccessMaster.CreatedAt,
            moduleAccessMaster.UpdatedBy,
            moduleAccessMaster.UpdatedAt,
            moduleAccessMaster.ClientId,
            moduleAccessMaster.TenantId,
            moduleAccessMaster.IsDeleted);
    }
}
