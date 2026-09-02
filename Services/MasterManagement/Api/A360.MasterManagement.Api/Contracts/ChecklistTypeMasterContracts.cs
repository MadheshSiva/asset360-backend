using ChecklistTypeMasterEntity = A360.MasterManagement.Domain.Entities.ChecklistTypeMaster;

namespace A360.MasterManagement.Api.Contracts;

public sealed record CreateChecklistTypeMasterRequest(
    string? AssetId,
    string? TypeName,
    string? ApplicableModule,
    bool IsActive,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public ChecklistTypeMasterEntity ToEntity(string typeId, string assetName)
    {
        return new ChecklistTypeMasterEntity
        {
            TypeId = typeId,
            AssetId = AssetId ?? string.Empty,
            AssetName = assetName,
            TypeName = TypeName ?? string.Empty,
            ApplicableModule = ApplicableModule ?? string.Empty,
            IsActive = IsActive,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateChecklistTypeMasterRequest(
    string? AssetId,
    string? TypeName,
    string? ApplicableModule,
    bool IsActive,
    string? UpdatedBy)
{
    public void ApplyTo(ChecklistTypeMasterEntity checklistTypeMaster, string assetName)
    {
        checklistTypeMaster.AssetId = AssetId ?? string.Empty;
        checklistTypeMaster.AssetName = assetName;
        checklistTypeMaster.TypeName = TypeName ?? string.Empty;
        checklistTypeMaster.ApplicableModule = ApplicableModule ?? string.Empty;
        checklistTypeMaster.IsActive = IsActive;
        checklistTypeMaster.UpdatedBy = UpdatedBy;
        checklistTypeMaster.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record ChecklistTypeMasterResponse(
    string Id,
    string TypeId,
    string AssetId,
    string AssetName,
    string TypeName,
    string ApplicableModule,
    bool IsActive,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static ChecklistTypeMasterResponse FromEntity(ChecklistTypeMasterEntity checklistTypeMaster)
    {
        return new ChecklistTypeMasterResponse(
            checklistTypeMaster.Id,
            checklistTypeMaster.TypeId,
            checklistTypeMaster.AssetId,
            checklistTypeMaster.AssetName,
            checklistTypeMaster.TypeName,
            checklistTypeMaster.ApplicableModule,
            checklistTypeMaster.IsActive,
            checklistTypeMaster.CreatedBy,
            checklistTypeMaster.CreatedAt,
            checklistTypeMaster.UpdatedBy,
            checklistTypeMaster.UpdatedAt,
            checklistTypeMaster.ClientId,
            checklistTypeMaster.TenantId,
            checklistTypeMaster.IsDeleted);
    }
}
