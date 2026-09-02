using EmailTemplateEntity = A360.VisitorManagement.Domain.Entities.EmailTemplate;

namespace A360.VisitorManagement.Api.Contracts;

public sealed record CreateEmailTemplateRequest(
    string? Name,
    string? Subject,
    string? Body,
    string? ClientId,
    string? TenantId)
{
    public EmailTemplateEntity ToEntity()
    {
        return new EmailTemplateEntity
        {
            Name = Name,
            Subject = Subject,
            Body = Body,
            ClientId = ClientId,
            TenantId = TenantId,
            IsDeleted = false
        };
    }
}

public sealed record UpdateEmailTemplateRequest(
    string? Subject,
    string? Body,
    string? UpdatedBy,
    string? Status)
{
    public void ApplyTo(EmailTemplateEntity template)
    {
        template.Subject = Subject;
        template.Body = Body;
        template.UpdatedBy = UpdatedBy;
        template.UpdatedAt = DateTime.UtcNow;
        if (!string.IsNullOrWhiteSpace(Status))
        {
            template.Status = Status;
        }
    }
}

public sealed record EmailTemplateResponse(
    string Id,
    string Name,
    string Subject,
    string Body,
    string? UpdatedBy,
    DateTime? UpdatedAt,
    string? TenantId,
    bool IsDeleted,
    string? Status)
{
    public static EmailTemplateResponse FromEntity(
        EmailTemplateEntity template)
    {
        return new EmailTemplateResponse(
            template.Id ?? string.Empty,
            template.Name ?? string.Empty,
            template.Subject ?? string.Empty,
            template.Body ?? string.Empty,
            template.UpdatedBy,
            template.UpdatedAt,
            template.TenantId,
            template.IsDeleted,
            template.Status);
    }
}
