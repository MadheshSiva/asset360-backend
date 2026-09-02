using RegistrationEntity = A360.VisitorManagement.Domain.Entities.VisitorRegistration;
using RegistrationDocumentEntity = A360.VisitorManagement.Domain.Entities.VisitorRegistrationDocument;

namespace A360.VisitorManagement.Api.Contracts;

public sealed record VisitorRegistrationDocumentDto(
    string? DocumentType,
    string? DocumentNumber,
    string? ExpiresOn,
    string? DocumentUrl)
{
    public RegistrationDocumentEntity ToEntity()
    {
        return new RegistrationDocumentEntity
        {
            DocumentType = DocumentType,
            DocumentNumber = DocumentNumber,
            ExpiresOn = ExpiresOn,
            DocumentUrl = DocumentUrl
        };
    }

    public static VisitorRegistrationDocumentDto FromEntity(
        RegistrationDocumentEntity document)
    {
        return new VisitorRegistrationDocumentDto(
            document.DocumentType,
            document.DocumentNumber,
            document.ExpiresOn,
            document.DocumentUrl);
    }
}

public sealed record CreateVisitorRegistrationRequest(
    string? VisitorType,
    string? FirstName,
    string? LastName,
    string? Email,
    string? MobileNo,
    string? IdTypeId,
    string? IdType,
    string? IdNo,
    string? Dob,
    string? CategoryId,
    string? Category,
    string? Gender,
    string? PhoneNo,
    string? NationalityId,
    string? Nationality,
    string? CompanyName,
    string? ContactName,
    string? ContactNo,
    string? CompanyEmail,
    string? Address,
    string? Telephone,
    string? TradeLicenseNo,
    string? TradeLicenseExpDate,
    List<VisitorRegistrationDocumentDto>? Documents,
    string? CreatedBy,
    string? Status,
    string? ClientId,
    string? TenantId)
{
    public RegistrationEntity ToEntity()
    {
        return new RegistrationEntity
        {
            VisitorType = VisitorType,
            FirstName = FirstName,
            LastName = LastName,
            Email = Email,
            MobileNo = MobileNo,
            IdTypeId = IdTypeId,
            IdType = IdType,
            IdNo = IdNo,
            Dob = Dob,
            CategoryId = CategoryId,
            Category = Category,
            Gender = Gender,
            PhoneNo = PhoneNo,
            NationalityId = NationalityId,
            Nationality = Nationality,
            CompanyName = CompanyName,
            ContactName = ContactName,
            ContactNo = ContactNo,
            CompanyEmail = CompanyEmail,
            Address = Address,
            Telephone = Telephone,
            TradeLicenseNo = TradeLicenseNo,
            TradeLicenseExpDate = TradeLicenseExpDate,
            Documents = Documents?.Select(d => d.ToEntity()).ToList() ?? [],
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow,
            Status = string.IsNullOrWhiteSpace(Status) ? "Pending" : Status,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateVisitorRegistrationRequest(
    string? VisitorType,
    string? FirstName,
    string? LastName,
    string? Email,
    string? MobileNo,
    string? IdTypeId,
    string? IdType,
    string? IdNo,
    string? Dob,
    string? CategoryId,
    string? Category,
    string? Gender,
    string? PhoneNo,
    string? NationalityId,
    string? Nationality,
    string? CompanyName,
    string? ContactName,
    string? ContactNo,
    string? CompanyEmail,
    string? Address,
    string? Telephone,
    string? TradeLicenseNo,
    string? TradeLicenseExpDate,
    List<VisitorRegistrationDocumentDto>? Documents,
    string? UpdatedBy,
    string? Status)
{
    public void ApplyTo(RegistrationEntity registration)
    {
        registration.VisitorType = VisitorType;
        registration.FirstName = FirstName;
        registration.LastName = LastName;
        registration.Email = Email;
        registration.MobileNo = MobileNo;
        registration.IdTypeId = IdTypeId;
        registration.IdType = IdType;
        registration.IdNo = IdNo;
        registration.Dob = Dob;
        registration.CategoryId = CategoryId;
        registration.Category = Category;
        registration.Gender = Gender;
        registration.PhoneNo = PhoneNo;
        registration.NationalityId = NationalityId;
        registration.Nationality = Nationality;
        registration.CompanyName = CompanyName;
        registration.ContactName = ContactName;
        registration.ContactNo = ContactNo;
        registration.CompanyEmail = CompanyEmail;
        registration.Address = Address;
        registration.Telephone = Telephone;
        registration.TradeLicenseNo = TradeLicenseNo;
        registration.TradeLicenseExpDate = TradeLicenseExpDate;
        registration.Documents = Documents?.Select(d => d.ToEntity()).ToList() ?? [];
        registration.UpdatedBy = UpdatedBy;
        registration.UpdatedAt = DateTime.UtcNow;
        registration.Status = Status;
    }
}

public sealed record VisitorRegistrationResponse(
    string Id,
    string VisitorType,
    string FirstName,
    string LastName,
    string Email,
    string MobileNo,
    string IdTypeId,
    string IdType,
    string IdNo,
    string? Dob,
    string? CategoryId,
    string Category,
    string? Gender,
    string? PhoneNo,
    string? NationalityId,
    string? Nationality,
    string CompanyName,
    string? ContactName,
    string? ContactNo,
    string CompanyEmail,
    string? Address,
    string? Telephone,
    string? TradeLicenseNo,
    string? TradeLicenseExpDate,
    List<VisitorRegistrationDocumentDto> Documents,
    string CreatedBy,
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string Password,
    string Status,
    string? TenantId,
    bool IsDeleted)
{
    public static VisitorRegistrationResponse FromEntity(
        RegistrationEntity registration)
    {
        return new VisitorRegistrationResponse(
            registration.Id ?? string.Empty,
            registration.VisitorType ?? string.Empty,
            registration.FirstName ?? string.Empty,
            registration.LastName ?? string.Empty,
            registration.Email ?? string.Empty,
            registration.MobileNo ?? string.Empty,
            registration.IdTypeId ?? string.Empty,
            registration.IdType ?? string.Empty,
            registration.IdNo ?? string.Empty,
            registration.Dob,
            registration.CategoryId,
            registration.Category ?? string.Empty,
            registration.Gender,
            registration.PhoneNo,
            registration.NationalityId,
            registration.Nationality,
            registration.CompanyName ?? string.Empty,
            registration.ContactName,
            registration.ContactNo,
            registration.CompanyEmail ?? string.Empty,
            registration.Address,
            registration.Telephone,
            registration.TradeLicenseNo,
            registration.TradeLicenseExpDate,
            registration.Documents.Select(VisitorRegistrationDocumentDto.FromEntity).ToList(),
            registration.CreatedBy ?? string.Empty,
            registration.CreatedAt,
            registration.UpdatedBy,
            registration.UpdatedAt,
            registration.Password ?? string.Empty,
            registration.Status ?? string.Empty,
            registration.TenantId,
            registration.IsDeleted);
    }
}
