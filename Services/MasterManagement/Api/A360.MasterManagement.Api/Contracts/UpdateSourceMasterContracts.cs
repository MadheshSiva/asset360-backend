using UpdateSourceMasterEntity = A360.MasterManagement.Domain.Entities.UpdateSourceMaster;

namespace A360.MasterManagement.Api.Contracts;

public sealed record CreateUpdateSourceMasterRequest(
    string? AssetId,
    string? SourceName,
    string? Description,
    string? CreatedBy,
    string? ClientId,
    string? TenantId,
    string? Status)
{
    public UpdateSourceMasterEntity ToEntity(string sourceId, string assetName)
    {
        return new UpdateSourceMasterEntity
        {
            SourceId = sourceId,
            AssetId = AssetId ?? string.Empty,
            AssetName = assetName,
            SourceName = SourceName ?? string.Empty,
            Description = Description ?? string.Empty,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            Status = Status,
            IsDeleted = false
        };
    }
}

public sealed record UpdateUpdateSourceMasterRequest(
    string? AssetId,
    string? SourceName,
    string? Description,
    string? UpdatedBy,
    string? Status)
{
    public void ApplyTo(UpdateSourceMasterEntity updateSourceMaster, string assetName)
    {
        updateSourceMaster.AssetId = AssetId ?? string.Empty;
        updateSourceMaster.AssetName = assetName;
        updateSourceMaster.SourceName = SourceName ?? string.Empty;
        updateSourceMaster.Description = Description ?? string.Empty;
        updateSourceMaster.UpdatedBy = UpdatedBy;
        updateSourceMaster.Status = Status;
        updateSourceMaster.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record UpdateSourceMasterResponse(
    string Id,
    string SourceId,
    string AssetId,
    string AssetName,
    string SourceName,
    string Description,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted,
    string? Status)
{
    public static UpdateSourceMasterResponse FromEntity(UpdateSourceMasterEntity updateSourceMaster)
    {
        return new UpdateSourceMasterResponse(
            updateSourceMaster.Id,
            updateSourceMaster.SourceId,
            updateSourceMaster.AssetId,
            updateSourceMaster.AssetName,
            updateSourceMaster.SourceName,
            updateSourceMaster.Description,
            updateSourceMaster.CreatedBy,
            updateSourceMaster.CreatedAt,
            updateSourceMaster.UpdatedBy,
            updateSourceMaster.UpdatedAt,
            updateSourceMaster.ClientId,
            updateSourceMaster.TenantId,
            updateSourceMaster.IsDeleted,
            updateSourceMaster.Status);
    }
}
