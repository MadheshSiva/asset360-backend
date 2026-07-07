using EmailTemplateEntity = A360.VisitorManagement.Domain.Entities.EmailTemplate;
using A360.Repository.Repositories;

namespace A360.VisitorManagement.Repository.Repositories;

public interface IEmailTemplateRepository
    : IMongoRepository<EmailTemplateEntity>
{
    Task<EmailTemplateEntity?> GetByNameAsync(
        string name,
        CancellationToken cancellationToken = default);
}
