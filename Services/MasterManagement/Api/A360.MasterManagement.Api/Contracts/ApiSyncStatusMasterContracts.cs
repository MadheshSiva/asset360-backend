using ApiSyncStatusMasterEntity = A360.MasterManagement.Domain.Entities.ApiSyncStatusMaster;

namespace A360.MasterManagement.Api.Contracts;

public sealed record CreateApiSyncStatusMasterRequest(
    string? AssetId,
    string? StatusName,
    string? StatusCode,
    string? Description,
    string? StatusType,
    bool IsFinalStatus,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public ApiSyncStatusMasterEntity ToEntity(string statusId, string assetName)
    {
        return new ApiSyncStatusMasterEntity
        {
            StatusId = statusId,
            AssetId = AssetId ?? string.Empty,
            AssetName = assetName,
            StatusName = StatusName ?? string.Empty,
            StatusCode = StatusCode ?? string.Empty,
            Description = Description ?? string.Empty,
            StatusType = StatusType ?? string.Empty,
            IsFinalStatus = IsFinalStatus,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateApiSyncStatusMasterRequest(
    string? AssetId,
    string? StatusName,
    string? StatusCode,
    string? Description,
    string? StatusType,
    bool IsFinalStatus,
    string? UpdatedBy)
{
    public void ApplyTo(ApiSyncStatusMasterEntity apiSyncStatusMaster, string assetName)
    {
        apiSyncStatusMaster.AssetId = AssetId ?? string.Empty;
        apiSyncStatusMaster.AssetName = assetName;
        apiSyncStatusMaster.StatusName = StatusName ?? string.Empty;
        apiSyncStatusMaster.StatusCode = StatusCode ?? string.Empty;
        apiSyncStatusMaster.Description = Description ?? string.Empty;
        apiSyncStatusMaster.StatusType = StatusType ?? string.Empty;
        apiSyncStatusMaster.IsFinalStatus = IsFinalStatus;
        apiSyncStatusMaster.UpdatedBy = UpdatedBy;
        apiSyncStatusMaster.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record ApiSyncStatusMasterResponse(
    string Id,
    string StatusId,
    string AssetId,
    string AssetName,
    string StatusName,
    string StatusCode,
    string Description,
    string StatusType,
    bool IsFinalStatus,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static ApiSyncStatusMasterResponse FromEntity(ApiSyncStatusMasterEntity apiSyncStatusMaster)
    {
        return new ApiSyncStatusMasterResponse(
            apiSyncStatusMaster.Id,
            apiSyncStatusMaster.StatusId,
            apiSyncStatusMaster.AssetId,
            apiSyncStatusMaster.AssetName,
            apiSyncStatusMaster.StatusName,
            apiSyncStatusMaster.StatusCode,
            apiSyncStatusMaster.Description,
            apiSyncStatusMaster.StatusType,
            apiSyncStatusMaster.IsFinalStatus,
            apiSyncStatusMaster.CreatedBy,
            apiSyncStatusMaster.CreatedAt,
            apiSyncStatusMaster.UpdatedBy,
            apiSyncStatusMaster.UpdatedAt,
            apiSyncStatusMaster.ClientId,
            apiSyncStatusMaster.TenantId,
            apiSyncStatusMaster.IsDeleted);
    }
}
