using AssetAuditAndVerificationEntity = A360.Asset.Domain.Entities.AssetAuditAndVerification;

namespace A360.Asset.Api.Contracts;

public sealed record CreateAssetAuditAndVerificationRequest(
    string? AssetId,
    string? AssetName,
    DateTime? AuditDate,
    string? AuditorDetails,
    string? PhysicalVerificationResult,
    string? DiscrepanciesFound,
    string? AuditHistoryLogs,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public AssetAuditAndVerificationEntity ToEntity(string auditVerificationId)
    {
        return new AssetAuditAndVerificationEntity
        {
            AuditVerificationId = auditVerificationId,
            AssetId = AssetId ?? string.Empty,
            AssetName = AssetName ?? string.Empty,
            AuditDate = AuditDate,
            AuditorDetails = AuditorDetails ?? string.Empty,
            PhysicalVerificationResult = PhysicalVerificationResult ?? string.Empty,
            DiscrepanciesFound = DiscrepanciesFound ?? string.Empty,
            AuditHistoryLogs = AuditHistoryLogs ?? string.Empty,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateAssetAuditAndVerificationRequest(
    string? AssetId,
    string? AssetName,
    DateTime? AuditDate,
    string? AuditorDetails,
    string? PhysicalVerificationResult,
    string? DiscrepanciesFound,
    string? AuditHistoryLogs,
    string? UpdatedBy)
{
    public void ApplyTo(AssetAuditAndVerificationEntity auditVerification)
    {
        auditVerification.AssetId = AssetId ?? string.Empty;
        auditVerification.AssetName = AssetName ?? string.Empty;
        auditVerification.AuditDate = AuditDate;
        auditVerification.AuditorDetails = AuditorDetails ?? string.Empty;
        auditVerification.PhysicalVerificationResult = PhysicalVerificationResult ?? string.Empty;
        auditVerification.DiscrepanciesFound = DiscrepanciesFound ?? string.Empty;
        auditVerification.AuditHistoryLogs = AuditHistoryLogs ?? string.Empty;
        auditVerification.UpdatedBy = UpdatedBy;
        auditVerification.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record AssetAuditAndVerificationResponse(
    string Id,
    string AuditVerificationId,
    string AssetId,
    string AssetName,
    DateTime? AuditDate,
    string AuditorDetails,
    string PhysicalVerificationResult,
    string DiscrepanciesFound,
    string AuditHistoryLogs,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static AssetAuditAndVerificationResponse FromEntity(AssetAuditAndVerificationEntity auditVerification)
    {
        return new AssetAuditAndVerificationResponse(
            auditVerification.Id,
            auditVerification.AuditVerificationId,
            auditVerification.AssetId,
            auditVerification.AssetName,
            auditVerification.AuditDate,
            auditVerification.AuditorDetails,
            auditVerification.PhysicalVerificationResult,
            auditVerification.DiscrepanciesFound,
            auditVerification.AuditHistoryLogs,
            auditVerification.CreatedBy,
            auditVerification.CreatedAt,
            auditVerification.UpdatedBy,
            auditVerification.UpdatedAt,
            auditVerification.ClientId,
            auditVerification.TenantId,
            auditVerification.IsDeleted);
    }
}
