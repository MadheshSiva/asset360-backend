using AssetAuditEntity = A360.Asset.Domain.Entities.AssetAudit;

namespace A360.Asset.Api.Contracts;

public sealed record CreateAssetAuditRequest(
    string? AssetId,
    string? AssetName,
    string? AuditCode,
    string? AuditName,
    DateTime? AuditStartDate,
    DateTime? AuditEndDate,
    bool Active,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public AssetAuditEntity ToEntity(string auditId)
    {
        return new AssetAuditEntity
        {
            AuditId = auditId,
            AssetId = AssetId ?? string.Empty,
            AssetName = AssetName ?? string.Empty,
            AuditCode = AuditCode ?? string.Empty,
            AuditName = AuditName ?? string.Empty,
            AuditStartDate = AuditStartDate,
            AuditEndDate = AuditEndDate,
            Active = Active,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateAssetAuditRequest(
    string? AssetId,
    string? AssetName,
    string? AuditCode,
    string? AuditName,
    DateTime? AuditStartDate,
    DateTime? AuditEndDate,
    bool Active,
    string? UpdatedBy)
{
    public void ApplyTo(AssetAuditEntity audit)
    {
        audit.AssetId = AssetId ?? string.Empty;
        audit.AssetName = AssetName ?? string.Empty;
        audit.AuditCode = AuditCode ?? string.Empty;
        audit.AuditName = AuditName ?? string.Empty;
        audit.AuditStartDate = AuditStartDate;
        audit.AuditEndDate = AuditEndDate;
        audit.Active = Active;
        audit.UpdatedBy = UpdatedBy;
        audit.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record AssetAuditResponse(
    string Id,
    string AuditId,
    string AssetId,
    string AssetName,
    string AuditCode,
    string AuditName,
    DateTime? AuditStartDate,
    DateTime? AuditEndDate,
    bool Active,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static AssetAuditResponse FromEntity(AssetAuditEntity audit)
    {
        return new AssetAuditResponse(
            audit.Id,
            audit.AuditId,
            audit.AssetId,
            audit.AssetName,
            audit.AuditCode,
            audit.AuditName,
            audit.AuditStartDate,
            audit.AuditEndDate,
            audit.Active,
            audit.CreatedBy,
            audit.CreatedAt,
            audit.UpdatedBy,
            audit.UpdatedAt,
            audit.ClientId,
            audit.TenantId,
            audit.IsDeleted);
    }
}
