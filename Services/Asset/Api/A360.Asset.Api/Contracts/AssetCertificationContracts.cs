using AssetCertificationEntity = A360.Asset.Domain.Entities.AssetCertification;

namespace A360.Asset.Api.Contracts;

public sealed record CreateAssetCertificationRequest(
    string? AssetId,
    string? AssetName,
    string? CertificationType,
    DateTime? IssuedDate,
    DateTime? ExpiryDate,
    string? InspectionLogs,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public AssetCertificationEntity ToEntity(string certificationId)
    {
        return new AssetCertificationEntity
        {
            CertificationId = certificationId,
            AssetId = AssetId ?? string.Empty,
            AssetName = AssetName ?? string.Empty,
            CertificationType = CertificationType ?? string.Empty,
            IssuedDate = IssuedDate,
            ExpiryDate = ExpiryDate,
            InspectionLogs = InspectionLogs ?? string.Empty,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateAssetCertificationRequest(
    string? AssetId,
    string? AssetName,
    string? CertificationType,
    DateTime? IssuedDate,
    DateTime? ExpiryDate,
    string? InspectionLogs,
    string? UpdatedBy)
{
    public void ApplyTo(AssetCertificationEntity certification)
    {
        certification.AssetId = AssetId ?? string.Empty;
        certification.AssetName = AssetName ?? string.Empty;
        certification.CertificationType = CertificationType ?? string.Empty;
        certification.IssuedDate = IssuedDate;
        certification.ExpiryDate = ExpiryDate;
        certification.InspectionLogs = InspectionLogs ?? string.Empty;
        certification.UpdatedBy = UpdatedBy;
        certification.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record AssetCertificationResponse(
    string Id,
    string CertificationId,
    string AssetId,
    string AssetName,
    string CertificationType,
    DateTime? IssuedDate,
    DateTime? ExpiryDate,
    string InspectionLogs,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static AssetCertificationResponse FromEntity(AssetCertificationEntity certification)
    {
        return new AssetCertificationResponse(
            certification.Id,
            certification.CertificationId,
            certification.AssetId,
            certification.AssetName,
            certification.CertificationType,
            certification.IssuedDate,
            certification.ExpiryDate,
            certification.InspectionLogs,
            certification.CreatedBy,
            certification.CreatedAt,
            certification.UpdatedBy,
            certification.UpdatedAt,
            certification.ClientId,
            certification.TenantId,
            certification.IsDeleted);
    }
}
