using AccessEntity = P360.People.Domain.Entities.Access;

namespace P360.People.Api.Contracts;

public sealed record CreateAccessRequest(
    string? GroupType,
    string? GroupName,
    List<string>? Members,
    List<string>? Readers,
    bool Status,
    DateTime FromDateTime,
    DateTime ToDateTime,
    string? CreatedBy,
    string? ClientId)
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
            CreatedAt = DateTime.UtcNow
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
    DateTime ToDateTime)
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
    DateTime CreatedAt)
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
            access.CreatedAt);
    }
}