using SeverityMasterEntity = A360.MasterManagement.Domain.Entities.SeverityMaster;

namespace A360.MasterManagement.Api.Contracts;

public sealed record CreateSeverityMasterRequest(
    string? AssetId,
    string? SeverityName,
    string? ColorCode,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public SeverityMasterEntity ToEntity(string severityId, string assetName)
    {
        return new SeverityMasterEntity
        {
            SeverityId = severityId,
            AssetId = AssetId ?? string.Empty,
            AssetName = assetName,
            SeverityName = SeverityName ?? string.Empty,
            ColorCode = ColorCode ?? string.Empty,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateSeverityMasterRequest(
    string? AssetId,
    string? SeverityName,
    string? ColorCode,
    string? UpdatedBy)
{
    public void ApplyTo(SeverityMasterEntity severityMaster, string assetName)
    {
        severityMaster.AssetId = AssetId ?? string.Empty;
        severityMaster.AssetName = assetName;
        severityMaster.SeverityName = SeverityName ?? string.Empty;
        severityMaster.ColorCode = ColorCode ?? string.Empty;
        severityMaster.UpdatedBy = UpdatedBy;
        severityMaster.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record SeverityMasterResponse(
    string Id,
    string SeverityId,
    string AssetId,
    string AssetName,
    string SeverityName,
    string ColorCode,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static SeverityMasterResponse FromEntity(SeverityMasterEntity severityMaster)
    {
        return new SeverityMasterResponse(
            severityMaster.Id,
            severityMaster.SeverityId,
            severityMaster.AssetId,
            severityMaster.AssetName,
            severityMaster.SeverityName,
            severityMaster.ColorCode,
            severityMaster.CreatedBy,
            severityMaster.CreatedAt,
            severityMaster.UpdatedBy,
            severityMaster.UpdatedAt,
            severityMaster.ClientId,
            severityMaster.TenantId,
            severityMaster.IsDeleted);
    }
}
