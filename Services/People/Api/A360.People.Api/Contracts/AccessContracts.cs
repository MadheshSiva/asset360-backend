using AccessEntity = A360.People.Domain.Entities.Access;

namespace A360.People.Api.Contracts;

public sealed record CreateAccessRequest(
    string? GroupType,
    string? GroupName,
    List<string>? Members,
    List<string>? Readers,
    bool Status,
    DateTime FromDateTime,
    DateTime ToDateTime,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public AccessEntity ToEntity()
    {
        return new AccessEntity
        {
            GroupType = GroupType,
            GroupName = GroupName,
            Members = Members ?? [],
            Readers = Readers ?? [],
            Status = Status,
            FromDateTime = FromDateTime,
            ToDateTime = ToDateTime,
            CreatedBy = CreatedBy,
            ClientId = ClientId,
            TenantId = TenantId,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false
        };
    }
}

public sealed record UpdateAccessRequest(
    string? GroupType,
    string? GroupName,
    List<string>? Members,
    List<string>? Readers,
    bool Status,
    DateTime FromDateTime,
    DateTime ToDateTime,
    string? UpdatedBy)
{
    public void ApplyTo(AccessEntity access)
    {
        access.GroupType = GroupType;
        access.GroupName = GroupName;
        access.Members = Members ?? [];
        access.Readers = Readers ?? [];
        access.Status = Status;
        access.FromDateTime = FromDateTime;
        access.ToDateTime = ToDateTime;
        access.UpdatedBy = UpdatedBy;
        access.UpdatedAt = DateTime.UtcNow;
    }
}

public sealed record AccessResponse(
    string Id,
    string GroupType,
    string GroupName,
    List<string> Members,
    List<string> Readers,
    bool Status,
    DateTime FromDateTime,
    DateTime ToDateTime,
    string CreatedBy,
    string ClientId,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? TenantId,
    bool IsDeleted)
{
    public static AccessResponse FromEntity(
        AccessEntity access)
    {
        return new AccessResponse(
            access.Id ?? string.Empty,
            access.GroupType ?? string.Empty,
            access.GroupName ?? string.Empty,
            access.Members ?? [],
            access.Readers ?? [],
            access.Status,
            access.FromDateTime,
            access.ToDateTime,
            access.CreatedBy ?? string.Empty,
            access.ClientId ?? string.Empty,
            access.CreatedAt,
            access.UpdatedBy,
            access.UpdatedAt,
            access.TenantId,
            access.IsDeleted);
    }
}