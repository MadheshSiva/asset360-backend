using StatusMasterEntity = A360.MasterManagement.Domain.Entities.StatusMaster;

namespace A360.MasterManagement.Api.Contracts;

public sealed record CreateStatusMasterRequest(
    string? AssetId,
    string? StatusName,
    string? ColorCode,
    string? AllowedTransitions,
    bool IsActive,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public StatusMasterEntity ToEntity(string statusId, string assetName)
    {
        return new StatusMasterEntity
        {
            StatusId = statusId,
            AssetId = AssetId ?? string.Empty,
            AssetName = assetName,
            StatusName = StatusName ?? string.Empty,
            ColorCode = ColorCode ?? string.Empty,
            AllowedTransitions = AllowedTransitions ?? string.Empty,
            IsActive = IsActive,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateStatusMasterRequest(
    string? AssetId,
    string? StatusName,
    string? ColorCode,
    string? AllowedTransitions,
    bool IsActive,
    string? UpdatedBy)
{
    public void ApplyTo(StatusMasterEntity statusMaster, string assetName)
    {
        statusMaster.AssetId = AssetId ?? string.Empty;
        statusMaster.AssetName = assetName;
        statusMaster.StatusName = StatusName ?? string.Empty;
        statusMaster.ColorCode = ColorCode ?? string.Empty;
        statusMaster.AllowedTransitions = AllowedTransitions ?? string.Empty;
        statusMaster.IsActive = IsActive;
        statusMaster.UpdatedBy = UpdatedBy;
        statusMaster.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record StatusMasterResponse(
    string Id,
    string StatusId,
    string AssetId,
    string AssetName,
    string StatusName,
    string ColorCode,
    string AllowedTransitions,
    bool IsActive,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static StatusMasterResponse FromEntity(StatusMasterEntity statusMaster)
    {
        return new StatusMasterResponse(
            statusMaster.Id,
            statusMaster.StatusId,
            statusMaster.AssetId,
            statusMaster.AssetName,
            statusMaster.StatusName,
            statusMaster.ColorCode,
            statusMaster.AllowedTransitions,
            statusMaster.IsActive,
            statusMaster.CreatedBy,
            statusMaster.CreatedAt,
            statusMaster.UpdatedBy,
            statusMaster.UpdatedAt,
            statusMaster.ClientId,
            statusMaster.TenantId,
            statusMaster.IsDeleted);
    }
}
