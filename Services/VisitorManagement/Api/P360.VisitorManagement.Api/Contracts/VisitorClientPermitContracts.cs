using ClientPermitEntity = P360.VisitorManagement.Domain.Entities.VisitorClientPermit;

namespace P360.VisitorManagement.Api.Contracts;

public sealed record CreateVisitorClientPermitRequest(
    string? ClientName,
    string? ClientEmail,
    string? SupportContactNo,
    string? SecurityContactNo,
    string? FireContactNo,
    string? CreatedBy)
{
    public ClientPermitEntity ToEntity()
    {
        return new ClientPermitEntity
        {
            ClientName = ClientName,
            ClientEmail = ClientEmail,
            SupportContactNo = SupportContactNo,
            SecurityContactNo = SecurityContactNo,
            FireContactNo = FireContactNo,
            CreatedBy = CreatedBy,
            CreatedAt = DateTime.UtcNow
        };
    }
}

public sealed record UpdateVisitorClientPermitRequest(
    string? ClientName,
    string? ClientEmail,
    string? SupportContactNo,
    string? SecurityContactNo,
    string? FireContactNo)
{
    public void ApplyTo(ClientPermitEntity clientPermit)
    {
        clientPermit.ClientName = ClientName;
        clientPermit.ClientEmail = ClientEmail;
        clientPermit.SupportContactNo = SupportContactNo;
        clientPermit.SecurityContactNo = SecurityContactNo;
        clientPermit.FireContactNo = FireContactNo;
    }
}

public sealed record VisitorClientPermitResponse(
    string Id,
    string ClientName,
    string ClientEmail,
    string SupportContactNo,
    string SecurityContactNo,
    string FireContactNo,
    string CreatedBy,
    DateTime CreatedAt)
{
    public static VisitorClientPermitResponse FromEntity(
        ClientPermitEntity clientPermit)
    {
        return new VisitorClientPermitResponse(
            clientPermit.Id ?? string.Empty,
            clientPermit.ClientName ?? string.Empty,
            clientPermit.ClientEmail ?? string.Empty,
            clientPermit.SupportContactNo ?? string.Empty,
            clientPermit.SecurityContactNo ?? string.Empty,
            clientPermit.FireContactNo ?? string.Empty,
            clientPermit.CreatedBy ?? string.Empty,
            clientPermit.CreatedAt);
    }
}
