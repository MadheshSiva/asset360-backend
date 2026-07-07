using PeopleEntity = P360.People.Domain.Entities.Visitor;

namespace P360.People.Api.Contracts;

public sealed record CreateVisitorRequest(
    string? ReferenceId,
    string? PhoneNo,
    string? Firstname,
    string? Lastname,
    string? Dept,
    DateTime StartDate,
    DateTime EndDate,
    string? Company,
    string? NationalId,
    string? SOWIdVehicleId,
    string? CardBadgeNumber,
    string? VisitorImage,
    string? CreatedBy,
    string? ClientId,
    string? Email,
    string? DocumentType,
    string? DocumentId,
    string? VisitorCompany,
    string? Action,
    string? HostPerson,
    string? HostPersonEmail)
{
    public PeopleEntity ToEntity()
    {
        return new PeopleEntity
        {
            ReferenceId = ReferenceId,
            PhoneNo = PhoneNo,
            Firstname = Firstname,
            Lastname = Lastname,
            Dept = Dept,
            StartDate = StartDate,
            EndDate = EndDate,
            Company = Company,
            NationalId = NationalId,
            SOWIdVehicleId = SOWIdVehicleId,
            CardBadgeNumber = CardBadgeNumber,
            VisitorImage = VisitorImage,
            CreatedBy = CreatedBy,
            ClientId = ClientId,
            Email = Email,
            DocumentType = DocumentType,
            DocumentId = DocumentId,
            VisitorCompany = VisitorCompany,
            Action = Action,
            HostPerson = HostPerson,
            HostPersonEmail = HostPersonEmail,
            CreatedAt = DateTime.UtcNow
        };
    }
}

public sealed record UpdateVisitorRequest(
    string? PhoneNo,
    string? Firstname,
    string? Lastname,
    string? Dept,
    string? IDNumber,
    DateTime StartDate,
    DateTime EndDate,
    string? Company,
    string? NationalId,
    string? SOWIdVehicleId,
    string? CardBadgeNumber,
    string? VisitorImage,
    string? Email,
    string? AuthCode,
    string? DocumentType,
    string? DocumentId,
    string? VisitorCompany,
    string? Action,
    string? HostPerson,
    string? HostPersonEmail)
{
    public void ApplyTo(PeopleEntity visitor)
    {
        visitor.PhoneNo = PhoneNo;
        visitor.Firstname = Firstname;
        visitor.Lastname = Lastname;
        visitor.Dept = Dept;
        visitor.IDNumber = IDNumber;
        visitor.StartDate = StartDate;
        visitor.EndDate = EndDate;
        visitor.Company = Company;
        visitor.NationalId = NationalId;
        visitor.SOWIdVehicleId = SOWIdVehicleId;
        visitor.CardBadgeNumber = CardBadgeNumber;
        visitor.VisitorImage = VisitorImage;
        visitor.Email = Email;
        visitor.AuthCode = AuthCode;
        visitor.DocumentType = DocumentType;
        visitor.DocumentId = DocumentId;
        visitor.VisitorCompany = VisitorCompany;
        visitor.Action = Action;
        visitor.HostPerson = HostPerson;
        visitor.HostPersonEmail = HostPersonEmail;
    }
}

public sealed record VisitorAuthCodeResponse(
    bool IsValid,
    string Message,
    VisitorResponse? Visitor)
{
    public static VisitorAuthCodeResponse Success(PeopleEntity visitor)
    {
        return new VisitorAuthCodeResponse(
            true,
            "Authorization code is valid.",
            VisitorResponse.FromEntity(visitor));
    }

    public static VisitorAuthCodeResponse Failure(string message)
    {
        return new VisitorAuthCodeResponse(
            false,
            message,
            null);
    }
}

public sealed record VisitorResponse(
    string Id,
    string ReferenceId,
    string PhoneNo,
    string Firstname,
    string Lastname,
    string Dept,
    string IDNumber,
    DateTime StartDate,
    DateTime EndDate,
    string Company,
    string NationalId,
    string SOWIdVehicleId,
    string CardBadgeNumber,
    string VisitorImage,
    string CreatedBy,
    DateTime CreatedAt,
    string ClientId,
    string Email,
    string AuthCode,
    string DocumentType,
    string DocumentId,
    string VisitorCompany,
    string Action,
    string HostPerson,
    string HostPersonEmail)
{
    public static VisitorResponse FromEntity(
        PeopleEntity visitor)
    {
        return new VisitorResponse(
            visitor.Id ?? string.Empty,
            visitor.ReferenceId ?? string.Empty,
            visitor.PhoneNo ?? string.Empty,
            visitor.Firstname ?? string.Empty,
            visitor.Lastname ?? string.Empty,
            visitor.Dept ?? string.Empty,
            visitor.IDNumber ?? string.Empty,
            visitor.StartDate,
            visitor.EndDate,
            visitor.Company ?? string.Empty,
            visitor.NationalId ?? string.Empty,
            visitor.SOWIdVehicleId ?? string.Empty,
            visitor.CardBadgeNumber ?? string.Empty,
            visitor.VisitorImage ?? string.Empty,
            visitor.CreatedBy ?? string.Empty,
            visitor.CreatedAt,
            visitor.ClientId ?? string.Empty,
            visitor.Email ?? string.Empty,
            visitor.AuthCode ?? string.Empty,
            visitor.DocumentType ?? string.Empty,
            visitor.DocumentId ?? string.Empty,
            visitor.VisitorCompany ?? string.Empty,
            visitor.Action ?? string.Empty,
            visitor.HostPerson ?? string.Empty,
            visitor.HostPersonEmail ?? string.Empty);
    }
}