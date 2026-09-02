using GroupEntity = A360.People.Domain.Entities.Group;

namespace A360.People.Api.Contracts;

public sealed record CreateGroupRequest(
    string? GroupType,
    string? GroupName,
    List<string>? Members,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public GroupEntity ToEntity()
    {
        return new GroupEntity
        {
            GroupType = GroupType,
            GroupName = GroupName,
            Members = Members ?? [],
            CreatedBy = CreatedBy,
            ClientId = ClientId,
            TenantId = TenantId,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false
        };
    }
}

public sealed record UpdateGroupRequest(
    string? GroupType,
    string? GroupName,
    List<string>? Members,
    string? UpdatedBy,
    string? Status)
{
    public void ApplyTo(GroupEntity group)
    {
        group.GroupType = GroupType;
        group.GroupName = GroupName;
        group.Members = Members ?? [];
        group.UpdatedBy = UpdatedBy;
        group.UpdatedAt = DateTime.UtcNow;
        if (!string.IsNullOrWhiteSpace(Status))
        {
            group.Status = Status;
        }
    }
}

public sealed record GroupResponse(
    string Id,
    string GroupType,
    string GroupName,
    List<string> Members,
    string CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? ClientId,
    string? TenantId,
    string? Status,
    bool IsDeleted)
{
    public static GroupResponse FromEntity(
        GroupEntity group)
    {
        return new GroupResponse(
            group.Id ?? string.Empty,
            group.GroupType ?? string.Empty,
            group.GroupName ?? string.Empty,
            group.Members ?? [],
            group.CreatedBy ?? string.Empty,
            group.CreatedAt,
            group.UpdatedBy,
            group.UpdatedAt,
            group.ClientId,
            group.TenantId,
            group.Status,
            group.IsDeleted);
    }
}