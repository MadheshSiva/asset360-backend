using AuditorDetailEntity = A360.MasterManagement.Domain.Entities.AuditorDetail;

namespace A360.MasterManagement.Api.Contracts;

public sealed record CreateAuditorDetailRequest(
    string? AssetId,
    string? AuditorName,
    string? EmployeeCode,
    string? Department,
    string? Email,
    string? Phone,
    string? CertificationType,
    string? Status,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public AuditorDetailEntity ToEntity(string auditorId, string assetName)
    {
        return new AuditorDetailEntity
        {
            AuditorId = auditorId,
            AssetId = AssetId ?? string.Empty,
            AssetName = assetName,
            AuditorName = AuditorName ?? string.Empty,
            EmployeeCode = EmployeeCode ?? string.Empty,
            Department = Department ?? string.Empty,
            Email = Email ?? string.Empty,
            Phone = Phone ?? string.Empty,
            CertificationType = CertificationType ?? string.Empty,
            Status = Status,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateAuditorDetailRequest(
    string? AssetId,
    string? AuditorName,
    string? EmployeeCode,
    string? Department,
    string? Email,
    string? Phone,
    string? CertificationType,
    string? Status,
    string? UpdatedBy)
{
    public void ApplyTo(AuditorDetailEntity auditorDetail, string assetName)
    {
        auditorDetail.AssetId = AssetId ?? string.Empty;
        auditorDetail.AssetName = assetName;
        auditorDetail.AuditorName = AuditorName ?? string.Empty;
        auditorDetail.EmployeeCode = EmployeeCode ?? string.Empty;
        auditorDetail.Department = Department ?? string.Empty;
        auditorDetail.Email = Email ?? string.Empty;
        auditorDetail.Phone = Phone ?? string.Empty;
        auditorDetail.CertificationType = CertificationType ?? string.Empty;
        auditorDetail.Status = Status;
        auditorDetail.UpdatedBy = UpdatedBy;
        auditorDetail.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record AuditorDetailResponse(
    string Id,
    string AuditorId,
    string AssetId,
    string AssetName,
    string AuditorName,
    string EmployeeCode,
    string Department,
    string Email,
    string Phone,
    string CertificationType,
    string? Status,
    string? CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    bool IsDeleted)
{
    public static AuditorDetailResponse FromEntity(AuditorDetailEntity auditorDetail)
    {
        return new AuditorDetailResponse(
            auditorDetail.Id,
            auditorDetail.AuditorId,
            auditorDetail.AssetId,
            auditorDetail.AssetName,
            auditorDetail.AuditorName,
            auditorDetail.EmployeeCode,
            auditorDetail.Department,
            auditorDetail.Email,
            auditorDetail.Phone,
            auditorDetail.CertificationType,
            auditorDetail.Status,
            auditorDetail.CreatedBy,
            auditorDetail.CreatedAt,
            auditorDetail.UpdatedBy,
            auditorDetail.UpdatedAt,
            auditorDetail.ClientId,
            auditorDetail.TenantId,
            auditorDetail.IsDeleted);
    }
}
