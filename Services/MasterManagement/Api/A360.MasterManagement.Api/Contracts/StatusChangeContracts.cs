using StatusChangeEntity = A360.MasterManagement.Domain.Entities.StatusChange;

namespace A360.MasterManagement.Api.Contracts;

public sealed record CreateStatusChangeRequest(
    string? AssetId,
    string? StatusName,
    string? StatusCode,
    int SequenceOrder,
    bool IsClosedStatus,
    string? AllowedTransitions,
    bool RequiresApproval,
    bool IsDefault,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public StatusChangeEntity ToEntity(string statusChangeId, string assetName)
    {
        return new StatusChangeEntity
        {
            StatusChangeId = statusChangeId,
            AssetId = AssetId ?? string.Empty,
            AssetName = assetName,
            StatusName = StatusName ?? string.Empty,
            StatusCode = StatusCode ?? string.Empty,
            SequenceOrder = SequenceOrder,
            IsClosedStatus = IsClosedStatus,
            AllowedTransitions = AllowedTransitions ?? string.Empty,
            RequiresApproval = RequiresApproval,
            IsDefault = IsDefault,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateStatusChangeRequest(
    string? AssetId,
    string? StatusName,
    string? StatusCode,
    int SequenceOrder,
    bool IsClosedStatus,
    string? AllowedTransitions,
    bool RequiresApproval,
    bool IsDefault,
    string? UpdatedBy)
{
    public void ApplyTo(StatusChangeEntity statusChange, string assetName)
    {
        statusChange.AssetId = AssetId ?? string.Empty;
        statusChange.AssetName = assetName;
        statusChange.StatusName = StatusName ?? string.Empty;
        statusChange.StatusCode = StatusCode ?? string.Empty;
        statusChange.SequenceOrder = SequenceOrder;
        statusChange.IsClosedStatus = IsClosedStatus;
        statusChange.AllowedTransitions = AllowedTransitions ?? string.Empty;
        statusChange.RequiresApproval = RequiresApproval;
        statusChange.IsDefault = IsDefault;
        statusChange.UpdatedBy = UpdatedBy;
        statusChange.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record StatusChangeResponse(
    string Id,
    string StatusChangeId,
    string AssetId,
    string AssetName,
    string StatusName,
    string StatusCode,
    int SequenceOrder,
    bool IsClosedStatus,
    string AllowedTransitions,
    bool RequiresApproval,
    bool IsDefault,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static StatusChangeResponse FromEntity(StatusChangeEntity statusChange)
    {
        return new StatusChangeResponse(
            statusChange.Id,
            statusChange.StatusChangeId,
            statusChange.AssetId,
            statusChange.AssetName,
            statusChange.StatusName,
            statusChange.StatusCode,
            statusChange.SequenceOrder,
            statusChange.IsClosedStatus,
            statusChange.AllowedTransitions,
            statusChange.RequiresApproval,
            statusChange.IsDefault,
            statusChange.CreatedBy,
            statusChange.CreatedAt,
            statusChange.UpdatedBy,
            statusChange.UpdatedAt,
            statusChange.ClientId,
            statusChange.TenantId,
            statusChange.IsDeleted);
    }
}
