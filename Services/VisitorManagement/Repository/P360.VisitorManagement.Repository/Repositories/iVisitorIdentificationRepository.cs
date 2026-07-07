using VisitorIdentificationEntity = P360.VisitorManagement.Domain.Entities.VisitorIdentification;
using P360.Repository.Repositories;

namespace P360.VisitorManagement.Repository.Repositories;

public interface IVisitorIdentificationRepository
    : IMongoRepository<VisitorIdentificationEntity>
{
    Task<IReadOnlyCollection<VisitorIdentificationEntity>> GetByIdentificationTypeAsync(
        string identificationType,
        CancellationToken cancellationToken = default);
}
