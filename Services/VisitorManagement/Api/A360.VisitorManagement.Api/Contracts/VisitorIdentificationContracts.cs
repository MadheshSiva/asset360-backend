using VisitorIdentificationEntity = A360.VisitorManagement.Domain.Entities.VisitorIdentification;

namespace A360.VisitorManagement.Api.Contracts;

public sealed record CreateVisitorIdentificationRequest(
    string? Name,
    string? IdentificationType,
    string? ReaderId,
    string? EntryExistId,
    string? EntryExistPoint,
    string? ReaderTypeId,
    string? ReaderTypeName,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public VisitorIdentificationEntity ToEntity()
    {
        return new VisitorIdentificationEntity
        {
            Name = Name,
            IdentificationType = IdentificationType!,
            ReaderId = ReaderId,
            EntryExistId = EntryExistId,
            EntryExistPoint = EntryExistPoint,
            ReaderTypeId = ReaderTypeId,
            ReaderTypeName = ReaderTypeName,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateVisitorIdentificationRequest(
    string? Name,
    string? IdentificationType,
    string? ReaderId,
    string? EntryExistId,
    string? EntryExistPoint,
    string? ReaderTypeId,
    string? ReaderTypeName,
    string? UpdatedBy,
    string? Status)
{
    public void ApplyTo(VisitorIdentificationEntity identification)
    {
        identification.Name = Name;
        identification.IdentificationType = IdentificationType!;
        identification.ReaderId = ReaderId;
        identification.EntryExistId = EntryExistId;
        identification.EntryExistPoint = EntryExistPoint;
        identification.ReaderTypeId = ReaderTypeId;
        identification.ReaderTypeName = ReaderTypeName;
        identification.UpdatedBy = UpdatedBy;
        identification.UpdatedAt = DateTime.UtcNow;
        if (!string.IsNullOrWhiteSpace(Status))
        {
            identification.Status = Status;
        }
    }
}

public sealed record VisitorIdentificationResponse(
    string Id,
    string Name,
    string IdentificationType,
    string ReaderId,
    string EntryExistId,
    string EntryExistPoint,
    string ReaderTypeId,
    string ReaderTypeName,
    string CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? TenantId,
    bool IsDeleted,
    string? Status)
{
    public static VisitorIdentificationResponse FromEntity(
        VisitorIdentificationEntity identification)
    {
        return new VisitorIdentificationResponse(
            identification.Id ?? string.Empty,
            identification.Name ?? string.Empty,
            identification.IdentificationType ?? string.Empty,
            identification.ReaderId ?? string.Empty,
            identification.EntryExistId ?? string.Empty,
            identification.EntryExistPoint ?? string.Empty,
            identification.ReaderTypeId ?? string.Empty,
            identification.ReaderTypeName ?? string.Empty,
            identification.CreatedBy ?? string.Empty,
            identification.CreatedAt,
            identification.UpdatedBy,
            identification.UpdatedAt,
            identification.TenantId,
            identification.IsDeleted,
            identification.Status);
    }
}
