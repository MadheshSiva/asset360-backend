using EmailTemplateEntity = P360.VisitorManagement.Domain.Entities.EmailTemplate;
using P360.Repository.Repositories;

namespace P360.VisitorManagement.Repository.Repositories;

public interface IEmailTemplateRepository
    : IMongoRepository<EmailTemplateEntity>
{
    Task<EmailTemplateEntity?> GetByNameAsync(
        string name,
        CancellationToken cancellationToken = default);
}
