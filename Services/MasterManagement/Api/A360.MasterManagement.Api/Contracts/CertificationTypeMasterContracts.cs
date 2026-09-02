using CertificationTypeMasterEntity = A360.MasterManagement.Domain.Entities.CertificationTypeMaster;

namespace A360.MasterManagement.Api.Contracts;

public sealed record CreateCertificationTypeMasterRequest(
    string? AssetId,
    string? CertificationName,
    string? CertificationCode,
    string? Description,
    string? ApplicableAssetType,
    string? IssuingAuthority,
    int ValidityPeriodDays,
    bool RenewalRequired,
    string? Status,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public CertificationTypeMasterEntity ToEntity(string certificationId, string assetName)
    {
        return new CertificationTypeMasterEntity
        {
            CertificationId = certificationId,
            AssetId = AssetId ?? string.Empty,
            AssetName = assetName,
            CertificationName = CertificationName ?? string.Empty,
            CertificationCode = CertificationCode ?? string.Empty,
            Description = Description ?? string.Empty,
            ApplicableAssetType = ApplicableAssetType ?? string.Empty,
            IssuingAuthority = IssuingAuthority ?? string.Empty,
            ValidityPeriodDays = ValidityPeriodDays,
            RenewalRequired = RenewalRequired,
            Status = Status,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateCertificationTypeMasterRequest(
    string? AssetId,
    string? CertificationName,
    string? CertificationCode,
    string? Description,
    string? ApplicableAssetType,
    string? IssuingAuthority,
    int ValidityPeriodDays,
    bool RenewalRequired,
    string? Status,
    string? UpdatedBy)
{
    public void ApplyTo(CertificationTypeMasterEntity certificationTypeMaster, string assetName)
    {
        certificationTypeMaster.AssetId = AssetId ?? string.Empty;
        certificationTypeMaster.AssetName = assetName;
        certificationTypeMaster.CertificationName = CertificationName ?? string.Empty;
        certificationTypeMaster.CertificationCode = CertificationCode ?? string.Empty;
        certificationTypeMaster.Description = Description ?? string.Empty;
        certificationTypeMaster.ApplicableAssetType = ApplicableAssetType ?? string.Empty;
        certificationTypeMaster.IssuingAuthority = IssuingAuthority ?? string.Empty;
        certificationTypeMaster.ValidityPeriodDays = ValidityPeriodDays;
        certificationTypeMaster.RenewalRequired = RenewalRequired;
        certificationTypeMaster.Status = Status;
        certificationTypeMaster.UpdatedBy = UpdatedBy;
        certificationTypeMaster.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record CertificationTypeMasterResponse(
    string Id,
    string CertificationId,
    string AssetId,
    string AssetName,
    string CertificationName,
    string CertificationCode,
    string Description,
    string ApplicableAssetType,
    string IssuingAuthority,
    int ValidityPeriodDays,
    bool RenewalRequired,
    string? Status,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static CertificationTypeMasterResponse FromEntity(CertificationTypeMasterEntity certificationTypeMaster)
    {
        return new CertificationTypeMasterResponse(
            certificationTypeMaster.Id,
            certificationTypeMaster.CertificationId,
            certificationTypeMaster.AssetId,
            certificationTypeMaster.AssetName,
            certificationTypeMaster.CertificationName,
            certificationTypeMaster.CertificationCode,
            certificationTypeMaster.Description,
            certificationTypeMaster.ApplicableAssetType,
            certificationTypeMaster.IssuingAuthority,
            certificationTypeMaster.ValidityPeriodDays,
            certificationTypeMaster.RenewalRequired,
            certificationTypeMaster.Status,
            certificationTypeMaster.CreatedBy,
            certificationTypeMaster.CreatedAt,
            certificationTypeMaster.UpdatedBy,
            certificationTypeMaster.UpdatedAt,
            certificationTypeMaster.ClientId,
            certificationTypeMaster.TenantId,
            certificationTypeMaster.IsDeleted);
    }
}
