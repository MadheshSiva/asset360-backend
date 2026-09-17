using PriorityEntity = A360.MasterManagement.Domain.Entities.Priority;

namespace A360.MasterManagement.Api.Contracts;

public sealed record CreatePriorityRequest(
    string? AssetId,
    string? PriorityName,
    string? ColorCode,
    string? SlaMapping,
    bool IsActive,
    string? CreatedBy,
    string? ClientId,
    string? TenantId,
    string? Status)
{
    public PriorityEntity ToEntity(string priorityId, string assetName)
    {
        return new PriorityEntity
        {
            PriorityId = priorityId,
            AssetId = AssetId ?? string.Empty,
            AssetName = assetName,
            PriorityName = PriorityName ?? string.Empty,
            ColorCode = ColorCode ?? string.Empty,
            SlaMapping = SlaMapping ?? string.Empty,
            IsActive = IsActive,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            Status = Status,
            IsDeleted = false
        };
    }
}

public sealed record UpdatePriorityRequest(
    string? AssetId,
    string? PriorityName,
    string? ColorCode,
    string? SlaMapping,
    bool IsActive,
    string? UpdatedBy,
    string? Status)
{
    public void ApplyTo(PriorityEntity priority, string assetName)
    {
        priority.AssetId = AssetId ?? string.Empty;
        priority.AssetName = assetName;
        priority.PriorityName = PriorityName ?? string.Empty;
        priority.ColorCode = ColorCode ?? string.Empty;
        priority.SlaMapping = SlaMapping ?? string.Empty;
        priority.IsActive = IsActive;
        priority.UpdatedBy = UpdatedBy;
        priority.Status = Status;
        priority.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record PriorityResponse(
    string Id,
    string PriorityId,
    string AssetId,
    string AssetName,
    string PriorityName,
    string ColorCode,
    string SlaMapping,
    bool IsActive,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted,
    string? Status)
{
    public static PriorityResponse FromEntity(PriorityEntity priority)
    {
        return new PriorityResponse(
            priority.Id,
            priority.PriorityId,
            priority.AssetId,
            priority.AssetName,
            priority.PriorityName,
            priority.ColorCode,
            priority.SlaMapping,
            priority.IsActive,
            priority.CreatedBy,
            priority.CreatedAt,
            priority.UpdatedBy,
            priority.UpdatedAt,
            priority.ClientId,
            priority.TenantId,
            priority.IsDeleted,
            priority.Status);
    }
}
