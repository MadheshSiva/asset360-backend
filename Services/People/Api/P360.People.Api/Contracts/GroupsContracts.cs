using GroupEntity = P360.People.Domain.Entities.Group;

namespace P360.People.Api.Contracts;

public sealed record CreateGroupRequest(
    string? GroupType,
    string? GroupName,
    List<string>? Members,
    string? CreatedBy)
{
    public GroupEntity ToEntity()
    {
        return new GroupEntity
        {
            GroupType = GroupType,
            GroupName = GroupName,
            Members = Members ?? [],
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow
        };
    }
}

public sealed record UpdateGroupRequest(
    string? GroupType,
    string? GroupName,
    List<string>? Members)
{
    public void ApplyTo(GroupEntity group)
    {
        group.GroupType = GroupType;
        group.GroupName = GroupName;
        group.Members = Members ?? [];
    }
}

public sealed record GroupResponse(
    string Id,
    string GroupType,
    string GroupName,
    List<string> Members,
    string CreatedBy,
    DateTime CreatedAt)
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
            group.CreatedAt);
    }
}