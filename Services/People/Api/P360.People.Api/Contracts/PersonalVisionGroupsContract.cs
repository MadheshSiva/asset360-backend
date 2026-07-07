using PersonalVisionGroupEntity = P360.People.Domain.Entities.PersonalVisionGroup;
using P360.People.Domain.Entities;

namespace P360.People.Api.Contracts;

public sealed record GroupMemberRequest(
    string? MemberID,
    string? MemberName);

public sealed record CreatePersonalVisionGroupRequest(
    string? ClientId,
    string? UserId,
    string? GroupType,
    string? GroupName,
    List<GroupMemberRequest>? Members,
    string? CreatedBy)
{
    public PersonalVisionGroupEntity ToEntity()
    {
        return new PersonalVisionGroupEntity
        {
            ClientId = ClientId,
            UserId = UserId,
            GroupType = GroupType,
            GroupName = GroupName,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            IsActive = true,
            Members = Members?
                .Select(x => new GroupMember
                {
                    MemberID = x.MemberID,
                    MemberName = x.MemberName
                })
                .ToList() ?? []
        };
    }
}

public sealed record UpdatePersonalVisionGroupRequest(
    string? GroupType,
    string? GroupName,
    List<GroupMemberRequest>? Members,
    string? ModifiedBy,
    bool IsActive)
{
    public void ApplyTo(
        PersonalVisionGroupEntity group)
    {
        group.GroupType = GroupType;
        group.GroupName = GroupName;
        group.ModifiedBy = ModifiedBy;
        group.ModifiedAt = DateTime.UtcNow;
        group.IsActive = IsActive;

        group.Members = Members?
            .Select(x => new GroupMember
            {
                MemberID = x.MemberID,
                MemberName = x.MemberName
            })
            .ToList() ?? [];
    }
}

public sealed record GroupMemberResponse(
    string MemberID,
    string MemberName);

public sealed record PersonalVisionGroupResponse(
    string Id,
    string ClientId,
    string UserId,
    string GroupType,
    string GroupName,
    List<GroupMemberResponse> Members,
    string CreatedBy,
    DateTime CreatedAt,
    string ModifiedBy,
    DateTime ModifiedAt,
    bool IsActive)
{
    public static PersonalVisionGroupResponse FromEntity(
        PersonalVisionGroupEntity group)
    {
        return new PersonalVisionGroupResponse(
            group.Id ?? string.Empty,
            group.ClientId ?? string.Empty,
            group.UserId ?? string.Empty,
            group.GroupType ?? string.Empty,
            group.GroupName ?? string.Empty,
            group.Members
                .Select(x => new GroupMemberResponse(
                    x.MemberID ?? string.Empty,
                    x.MemberName ?? string.Empty))
                .ToList(),
            group.CreatedBy ?? string.Empty,
            group.CreatedAt,
            group.ModifiedBy ?? string.Empty,
            group.ModifiedAt,
            group.IsActive);
    }
}