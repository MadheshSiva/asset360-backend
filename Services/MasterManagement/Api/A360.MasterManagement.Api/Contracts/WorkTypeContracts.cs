using WorkTypeEntity = A360.MasterManagement.Domain.Entities.WorkType;

namespace A360.MasterManagement.Api.Contracts;

public sealed record CreateWorkTypeRequest(
    string? AssetId,
    string? WorkTypeName,
    string? Description,
    bool IsActive,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public WorkTypeEntity ToEntity(string workTypeId, string assetName)
    {
        return new WorkTypeEntity
        {
            WorkTypeId = workTypeId,
            AssetId = AssetId ?? string.Empty,
            AssetName = assetName,
            WorkTypeName = WorkTypeName ?? string.Empty,
            Description = Description ?? string.Empty,
            IsActive = IsActive,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateWorkTypeRequest(
    string? AssetId,
    string? WorkTypeName,
    string? Description,
    bool IsActive,
    string? UpdatedBy)
{
    public void ApplyTo(WorkTypeEntity workType, string assetName)
    {
        workType.AssetId = AssetId ?? string.Empty;
        workType.AssetName = assetName;
        workType.WorkTypeName = WorkTypeName ?? string.Empty;
        workType.Description = Description ?? string.Empty;
        workType.IsActive = IsActive;
        workType.UpdatedBy = UpdatedBy;
        workType.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record WorkTypeResponse(
    string Id,
    string WorkTypeId,
    string AssetId,
    string AssetName,
    string WorkTypeName,
    string Description,
    bool IsActive,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static WorkTypeResponse FromEntity(WorkTypeEntity workType)
    {
        return new WorkTypeResponse(
            workType.Id,
            workType.WorkTypeId,
            workType.AssetId,
            workType.AssetName,
            workType.WorkTypeName,
            workType.Description,
            workType.IsActive,
            workType.CreatedBy,
            workType.CreatedAt,
            workType.UpdatedBy,
            workType.UpdatedAt,
            workType.ClientId,
            workType.TenantId,
            workType.IsDeleted);
    }
}
