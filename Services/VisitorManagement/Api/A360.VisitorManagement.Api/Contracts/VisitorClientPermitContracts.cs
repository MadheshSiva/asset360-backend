using ClientPermitEntity = A360.VisitorManagement.Domain.Entities.VisitorClientPermit;

namespace A360.VisitorManagement.Api.Contracts;

public sealed record CreateVisitorClientPermitRequest(
    string? ClientName,
    string? ClientEmail,
    string? SupportContactNo,
    string? SecurityContactNo,
    string? FireContactNo,
    string? CreatedBy,
    string? ClientId,
    string? TenantId)
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
            CreatedAt = DateTime.UtcNow,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateVisitorClientPermitRequest(
    string? ClientName,
    string? ClientEmail,
    string? SupportContactNo,
    string? SecurityContactNo,
    string? FireContactNo,
    string? UpdatedBy,
    string? Status)
{
    public void ApplyTo(ClientPermitEntity clientPermit)
    {
        clientPermit.ClientName = ClientName;
        clientPermit.ClientEmail = ClientEmail;
        clientPermit.SupportContactNo = SupportContactNo;
        clientPermit.SecurityContactNo = SecurityContactNo;
        clientPermit.FireContactNo = FireContactNo;
        clientPermit.UpdatedBy = UpdatedBy;
        clientPermit.UpdatedAt = DateTime.UtcNow;
        if (!string.IsNullOrWhiteSpace(Status))
        {
            clientPermit.Status = Status;
        }
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
    DateTime? CreatedAt,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? TenantId,
    bool IsDeleted,
    string? Status)
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
            clientPermit.CreatedAt,
            clientPermit.UpdatedBy,
            clientPermit.UpdatedAt,
            clientPermit.TenantId,
            clientPermit.IsDeleted,
            clientPermit.Status);
    }
}
