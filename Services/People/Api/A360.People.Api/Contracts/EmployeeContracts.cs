using PeopleEntity = A360.People.Domain.Entities.Employee;

namespace A360.People.Api.Contracts;

public sealed record CreateEmployeeRequest(
    string? ReferenceId,
    string? Firstname,
    string? Lastname,
    string? Dept,
    string? Role,
    string? PhoneNo,
    string? EmployeeImage,
    string? CreatedBy,
    string? ClientId,
    string? IDNumber,
    DateTime StartDate,
    DateTime EndDate,
    string? Company,
    string? NationalId,
    string? SOWIdVehicleId,
    string? CardBadgeNumber,
    string? Variables,
    string? TenantId)
{
    public PeopleEntity ToEntity()
    {
        return new PeopleEntity
        {
            ReferenceId = ReferenceId,
            Firstname = Firstname,
            Lastname = Lastname,
            Dept = Dept,
            Role = Role,
            PhoneNo = PhoneNo,
            EmployeeImage = EmployeeImage,
            CreatedBy = CreatedBy,
            ClientId = ClientId,
            TenantId = TenantId,
            IDNumber = IDNumber,
            StartDate = StartDate,
            EndDate = EndDate,
            Company = Company,
            NationalId = NationalId,
            SOWIdVehicleId = SOWIdVehicleId,
            CardBadgeNumber = CardBadgeNumber,
            Variables = Variables,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false
        };
    }
}

public sealed record UpdateEmployeeRequest(
    string? Firstname,
    string? Lastname,
    string? Dept,
    string? Role,
    string? PhoneNo,
    string? EmployeeImage,
    string? IDNumber,
    DateTime StartDate,
    DateTime EndDate,
    string? Company,
    string? NationalId,
    string? SOWIdVehicleId,
    string? CardBadgeNumber,
    string? Variables,
    string? UpdatedBy,
    string? Status)
{
    public void ApplyTo(PeopleEntity employee)
    {
        employee.Firstname = Firstname;
        employee.Lastname = Lastname;
        employee.Dept = Dept;
        employee.Role = Role;
        employee.PhoneNo = PhoneNo;
        employee.EmployeeImage = EmployeeImage;
        employee.IDNumber = IDNumber;
        employee.StartDate = StartDate;
        employee.EndDate = EndDate;
        employee.Company = Company;
        employee.NationalId = NationalId;
        employee.SOWIdVehicleId = SOWIdVehicleId;
        employee.CardBadgeNumber = CardBadgeNumber;
        employee.Variables = Variables;
        employee.UpdatedBy = UpdatedBy;
        employee.UpdatedAt = DateTime.UtcNow;
        if (!string.IsNullOrWhiteSpace(Status))
        {
            employee.Status = Status;
        }
    }
}

public sealed record EmployeeResponse(
    string Id,
    string ReferenceId,
    string Firstname,
    string Lastname,
    string Dept,
    string Role,
    string PhoneNo,
    string EmployeeImage,
    string CreatedBy,
    DateTime? CreatedAt,
    string ClientId,
    string IDNumber,
    DateTime StartDate,
    DateTime EndDate,
    string Company,
    string NationalId,
    string SOWIdVehicleId,
    string CardBadgeNumber,
    string Variables,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? TenantId,
    string? Status,
    bool IsDeleted)
{
    public static EmployeeResponse FromEntity(
        PeopleEntity employee)
    {
        return new EmployeeResponse(
            employee.Id ?? string.Empty,
            employee.ReferenceId ?? string.Empty,
            employee.Firstname ?? string.Empty,
            employee.Lastname ?? string.Empty,
            employee.Dept ?? string.Empty,
            employee.Role ?? string.Empty,
            employee.PhoneNo ?? string.Empty,
            employee.EmployeeImage ?? string.Empty,
            employee.CreatedBy ?? string.Empty,
            employee.CreatedAt,
            employee.ClientId ?? string.Empty,
            employee.IDNumber ?? string.Empty,
            employee.StartDate,
            employee.EndDate,
            employee.Company ?? string.Empty,
            employee.NationalId ?? string.Empty,
            employee.SOWIdVehicleId ?? string.Empty,
            employee.CardBadgeNumber ?? string.Empty,
            employee.Variables ?? string.Empty,
            employee.UpdatedBy,
            employee.UpdatedAt,
            employee.TenantId,
            employee.Status,
            employee.IsDeleted);
    }
}