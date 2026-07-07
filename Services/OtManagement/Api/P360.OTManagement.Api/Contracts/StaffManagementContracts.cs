using StaffEntity = P360.OTManagement.Domain.Entities.StaffManagement;

namespace P360.OTManagement.Api.Contracts;

public sealed record CreateStaffManagementRequest(
    string? StaffId,
    string? StaffName,
    string? Role,
    string? Department,
    string? TagId,
    string? ContactNumber,
    string? Shift,
    bool Status)
{
    public StaffEntity ToEntity()
    {
        return new StaffEntity
        {
            StaffId = StaffId,
            StaffName = StaffName,
            Role = Role,
            Department = Department,
            TagId = TagId,
            ContactNumber = ContactNumber,
            Shift = Shift,
            Status = Status,
            CreatedAt = DateTime.UtcNow
        };
    }
}

public sealed record UpdateStaffManagementRequest(
    string? StaffId,
    string? StaffName,
    string? Role,
    string? Department,
    string? TagId,
    string? ContactNumber,
    string? Shift,
    bool Status)
{
    public void UpdateEntity(StaffEntity entity)
    {
        entity.StaffId = StaffId!;
        entity.StaffName = StaffName!;
        entity.Role = Role!;
        entity.Department = Department!;
        entity.TagId = TagId!;
        entity.ContactNumber = ContactNumber!;
        entity.Shift = Shift!;
        entity.Status = Status;
       
    }
}

public sealed record StaffManagementResponse(
    string Id,
    string StaffId,
    string StaffName,
    string Role,
    string Department,
    string TagId,
    string ContactNumber,
    string Shift,
    bool Status)
{
    public static StaffManagementResponse FromEntity(
        StaffEntity entity)
    {
        return new StaffManagementResponse(
            entity.Id,
            entity.StaffId,
            entity.StaffName,
            entity.Role,
            entity.Department,
            entity.TagId,
            entity.ContactNumber,
            entity.Shift,
            entity.Status);
    }
}