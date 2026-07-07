using RegistrationEntity = A360.VisitorManagement.Domain.Entities.VisitorRegistration;
using A360.Repository.Repositories;

namespace A360.VisitorManagement.Repository.Repositories;

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
