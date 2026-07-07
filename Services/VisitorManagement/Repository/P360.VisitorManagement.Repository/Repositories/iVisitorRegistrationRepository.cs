using RegistrationEntity = P360.VisitorManagement.Domain.Entities.VisitorRegistration;
using P360.Repository.Repositories;

namespace P360.VisitorManagement.Repository.Repositories;

public interface IVisitorRegistrationRepository
    : IMongoRepository<RegistrationEntity>
{
    Task<RegistrationEntity?> GetByEmailAsync(
        string email,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<RegistrationEntity>> GetByVisitorTypeAsync(
        string visitorType,
        CancellationToken cancellationToken = default);
}
