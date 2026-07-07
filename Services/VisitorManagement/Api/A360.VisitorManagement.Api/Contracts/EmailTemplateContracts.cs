using EmailTemplateEntity = A360.VisitorManagement.Domain.Entities.EmailTemplate;

namespace A360.VisitorManagement.Api.Contracts;

public sealed record CreateEmailTemplateRequest(
    string? Name,
    string? Subject,
    string? Body)
{
    public EmailTemplateEntity ToEntity()
    {
        return new EmailTemplateEntity
        {
            Name = Name,
            Subject = Subject,
            Body = Body
        };
    }
}

public sealed record UpdateEmailTemplateRequest(
    string? Subject,
    string? Body)
{
    public void ApplyTo(EmailTemplateEntity template)
    {
        template.Subject = Subject;
        template.Body = Body;
    }
}

public sealed record EmailTemplateResponse(
    string Id,
    string Name,
    string Subject,
    string Body)
{
    public static EmailTemplateResponse FromEntity(
        EmailTemplateEntity template)
    {
        return new EmailTemplateResponse(
            template.Id ?? string.Empty,
            template.Name ?? string.Empty,
            template.Subject ?? string.Empty,
            template.Body ?? string.Empty);
    }
}
