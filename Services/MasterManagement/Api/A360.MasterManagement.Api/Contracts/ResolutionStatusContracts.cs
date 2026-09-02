using ResolutionStatusEntity = A360.MasterManagement.Domain.Entities.ResolutionStatus;

namespace A360.MasterManagement.Api.Contracts;

public sealed record CreateResolutionStatusRequest(
    string? AssetId,
    string? StatusName,
    string? StatusCode,
    string? Description,
    bool IsFinalStatus,
    string? StatusCategory,
    int SequenceOrder,
    string? StatusColor,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public ResolutionStatusEntity ToEntity(string statusId, string assetName)
    {
        return new ResolutionStatusEntity
        {
            StatusId = statusId,
            AssetId = AssetId ?? string.Empty,
            AssetName = assetName,
            StatusName = StatusName ?? string.Empty,
            StatusCode = StatusCode ?? string.Empty,
            Description = Description ?? string.Empty,
            IsFinalStatus = IsFinalStatus,
            StatusCategory = StatusCategory ?? string.Empty,
            SequenceOrder = SequenceOrder,
            StatusColor = StatusColor ?? string.Empty,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateResolutionStatusRequest(
    string? AssetId,
    string? StatusName,
    string? StatusCode,
    string? Description,
    bool IsFinalStatus,
    string? StatusCategory,
    int SequenceOrder,
    string? StatusColor,
    string? UpdatedBy)
{
    public void ApplyTo(ResolutionStatusEntity resolutionStatus, string assetName)
    {
        resolutionStatus.AssetId = AssetId ?? string.Empty;
        resolutionStatus.AssetName = assetName;
        resolutionStatus.StatusName = StatusName ?? string.Empty;
        resolutionStatus.StatusCode = StatusCode ?? string.Empty;
        resolutionStatus.Description = Description ?? string.Empty;
        resolutionStatus.IsFinalStatus = IsFinalStatus;
        resolutionStatus.StatusCategory = StatusCategory ?? string.Empty;
        resolutionStatus.SequenceOrder = SequenceOrder;
        resolutionStatus.StatusColor = StatusColor ?? string.Empty;
        resolutionStatus.UpdatedBy = UpdatedBy;
        resolutionStatus.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record ResolutionStatusResponse(
    string Id,
    string StatusId,
    string AssetId,
    string AssetName,
    string StatusName,
    string StatusCode,
    string Description,
    bool IsFinalStatus,
    string StatusCategory,
    int SequenceOrder,
    string StatusColor,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static ResolutionStatusResponse FromEntity(ResolutionStatusEntity resolutionStatus)
    {
        return new ResolutionStatusResponse(
            resolutionStatus.Id,
            resolutionStatus.StatusId,
            resolutionStatus.AssetId,
            resolutionStatus.AssetName,
            resolutionStatus.StatusName,
            resolutionStatus.StatusCode,
            resolutionStatus.Description,
            resolutionStatus.IsFinalStatus,
            resolutionStatus.StatusCategory,
            resolutionStatus.SequenceOrder,
            resolutionStatus.StatusColor,
            resolutionStatus.CreatedBy,
            resolutionStatus.CreatedAt,
            resolutionStatus.UpdatedBy,
            resolutionStatus.UpdatedAt,
            resolutionStatus.ClientId,
            resolutionStatus.TenantId,
            resolutionStatus.IsDeleted);
    }
}
