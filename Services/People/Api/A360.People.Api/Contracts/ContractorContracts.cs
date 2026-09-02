using ContractorEntity = A360.People.Domain.Entities.Contractor;

namespace A360.People.Api.Contracts;

public sealed record CreateContractorRequest(
    string? ReferenceId,
    string? ContractorName,
    string? ContractorId,
    string? CompanyName,
    string? ProjectName,
    string? Address,
    DateTime ContractStart,
    DateTime ContractEnd,
    string? PhoneNo,
    string? Nationality,
    string? VehicleName,
    string? VehicleId,
    string? ContractorImage,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
{
    public ContractorEntity ToEntity()
    {
        return new ContractorEntity
        {
            ReferenceId = ReferenceId,
            ContractorName = ContractorName,
            ContractorId = ContractorId,
            CompanyName = CompanyName,
            ProjectName = ProjectName,
            Address = Address,
            ContractStart = ContractStart,
            ContractEnd = ContractEnd,
            PhoneNo = PhoneNo,
            Nationality = Nationality,
            VehicleName = VehicleName,
            VehicleId = VehicleId,
            ContractorImage = ContractorImage,
            CreatedBy = CreatedBy,
            ClientId = ClientId,
            TenantId = TenantId,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false
        };
    }
}

public sealed record UpdateContractorRequest(
    string? ContractorName,
    string? ContractorId,
    string? CompanyName,
    string? ProjectName,
    string? Address,
    DateTime ContractStart,
    DateTime ContractEnd,
    string? PhoneNo,
    string? Nationality,
    string? VehicleName,
    string? VehicleId,
    string? ContractorImage,
    string? UpdatedBy,
    string? Status)
{
    public void ApplyTo(ContractorEntity contractor)
    {
        contractor.ContractorName = ContractorName;
        contractor.ContractorId = ContractorId;
        contractor.CompanyName = CompanyName;
        contractor.ProjectName = ProjectName;
        contractor.Address = Address;
        contractor.ContractStart = ContractStart;
        contractor.ContractEnd = ContractEnd;
        contractor.PhoneNo = PhoneNo;
        contractor.Nationality = Nationality;
        contractor.VehicleName = VehicleName;
        contractor.VehicleId = VehicleId;
        contractor.ContractorImage = ContractorImage;
        contractor.UpdatedBy = UpdatedBy;
        contractor.UpdatedAt = DateTime.UtcNow;
        if (!string.IsNullOrWhiteSpace(Status))
        {
            contractor.Status = Status;
        }
    }
}

public sealed record ContractorResponse(
    string Id,
    string ReferenceId,
    string ContractorName,
    string ContractorId,
    string CompanyName,
    string ProjectName,
    string Address,
    DateTime ContractStart,
    DateTime ContractEnd,
    string PhoneNo,
    string Nationality,
    string VehicleName,
    string VehicleId,
    string ContractorImage,
    string CreatedBy,
    DateTime? CreatedAt,
    string ClientId,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? TenantId,
    string? Status,
    bool IsDeleted)
{
    public static ContractorResponse FromEntity(
        ContractorEntity contractor)
    {
        return new ContractorResponse(
            contractor.Id ?? string.Empty,
            contractor.ReferenceId ?? string.Empty,
            contractor.ContractorName ?? string.Empty,
            contractor.ContractorId ?? string.Empty,
            contractor.CompanyName ?? string.Empty,
            contractor.ProjectName ?? string.Empty,
            contractor.Address ?? string.Empty,
            contractor.ContractStart,
            contractor.ContractEnd,
            contractor.PhoneNo ?? string.Empty,
            contractor.Nationality ?? string.Empty,
            contractor.VehicleName ?? string.Empty,
            contractor.VehicleId ?? string.Empty,
            contractor.ContractorImage ?? string.Empty,
            contractor.CreatedBy ?? string.Empty,
            contractor.CreatedAt,
            contractor.ClientId ?? string.Empty,
            contractor.UpdatedBy,
            contractor.UpdatedAt,
            contractor.TenantId,
            contractor.Status,
            contractor.IsDeleted);
    }
}