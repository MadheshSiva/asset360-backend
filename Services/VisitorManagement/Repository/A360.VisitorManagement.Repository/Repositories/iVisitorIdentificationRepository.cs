using VisitorIdentificationEntity = A360.VisitorManagement.Domain.Entities.VisitorIdentification;
using A360.Repository.Repositories;

namespace A360.VisitorManagement.Repository.Repositories;

public interface IVisitorIdentificationRepository
    : IMongoRepository<VisitorIdentificationEntity>
{
    Task<IReadOnlyCollection<VisitorIdentificationEntity>> GetByIdentificationTypeAsync(
        string identificationType,
        CancellationToken cancellationToken = default);
}
