using PersonalVisionGroupEntity = A360.People.Domain.Entities.PersonalVisionGroup;
using A360.People.Domain.Entities;

namespace A360.People.Api.Contracts;

public sealed record GroupMemberRequest(
    string? MemberID,
    string? MemberName);

public sealed record CreatePersonalVisionGroupRequest(
    string? ClientId,
    string? UserId,
    string? GroupType,
    string? GroupName,
    List<GroupMemberRequest>? Members,
    string? CreatedBy,
    string? TenantId)
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
            TenantId = TenantId,
            IsActive = true,
            IsDeleted = false,
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
    string? UpdatedBy,
    bool IsActive,
    string? Status)
{
    public void ApplyTo(
        PersonalVisionGroupEntity group)
    {
        group.GroupType = GroupType;
        group.GroupName = GroupName;
        group.UpdatedBy = UpdatedBy;
        group.UpdatedAt = DateTime.UtcNow;
        group.IsActive = IsActive;
        if (!string.IsNullOrWhiteSpace(Status))
        {
            group.Status = Status;
        }

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
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    bool IsActive,
    string? TenantId,
    string? Status,
    bool IsDeleted)
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
            group.UpdatedBy,
            group.UpdatedAt,
            group.IsActive,
            group.TenantId,
            group.Status,
            group.IsDeleted);
    }
}