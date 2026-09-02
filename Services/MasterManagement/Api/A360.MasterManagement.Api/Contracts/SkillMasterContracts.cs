using SkillMasterEntity = A360.MasterManagement.Domain.Entities.SkillMaster;

namespace A360.MasterManagement.Api.Contracts;

public sealed record CreateSkillMasterRequest(
    string? AssetId,
    string? SkillName,
    string? SkillLevel,
    bool CertificationRequired,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public SkillMasterEntity ToEntity(string skillId, string assetName)
    {
        return new SkillMasterEntity
        {
            SkillId = skillId,
            AssetId = AssetId ?? string.Empty,
            AssetName = assetName,
            SkillName = SkillName ?? string.Empty,
            SkillLevel = SkillLevel ?? string.Empty,
            CertificationRequired = CertificationRequired,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateSkillMasterRequest(
    string? AssetId,
    string? SkillName,
    string? SkillLevel,
    bool CertificationRequired,
    string? UpdatedBy)
{
    public void ApplyTo(SkillMasterEntity skillMaster, string assetName)
    {
        skillMaster.AssetId = AssetId ?? string.Empty;
        skillMaster.AssetName = assetName;
        skillMaster.SkillName = SkillName ?? string.Empty;
        skillMaster.SkillLevel = SkillLevel ?? string.Empty;
        skillMaster.CertificationRequired = CertificationRequired;
        skillMaster.UpdatedBy = UpdatedBy;
        skillMaster.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record SkillMasterResponse(
    string Id,
    string SkillId,
    string AssetId,
    string AssetName,
    string SkillName,
    string SkillLevel,
    bool CertificationRequired,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static SkillMasterResponse FromEntity(SkillMasterEntity skillMaster)
    {
        return new SkillMasterResponse(
            skillMaster.Id,
            skillMaster.SkillId,
            skillMaster.AssetId,
            skillMaster.AssetName,
            skillMaster.SkillName,
            skillMaster.SkillLevel,
            skillMaster.CertificationRequired,
            skillMaster.CreatedBy,
            skillMaster.CreatedAt,
            skillMaster.UpdatedBy,
            skillMaster.UpdatedAt,
            skillMaster.ClientId,
            skillMaster.TenantId,
            skillMaster.IsDeleted);
    }
}
