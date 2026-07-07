using VisitorEntity = A360.People.Domain.Entities.Visitor;
using A360.Repository.Repositories;

namespace A360.People.Repository.Repositories;

public interface IVisitorRepository
    : IMongoRepository<VisitorEntity>
{
    Task<IReadOnlyCollection<VisitorEntity>> GetByEmailAsync(
        string clientId,
        string email,
        CancellationToken cancellationToken = default);

    Task<VisitorEntity?> GetByAuthCodeAsync(
        string authCode,
        CancellationToken cancellationToken = default);
}