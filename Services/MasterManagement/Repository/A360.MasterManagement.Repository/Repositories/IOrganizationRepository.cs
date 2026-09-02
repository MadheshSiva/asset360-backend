using A360.Repository.Repositories;
using OrganizationEntity = A360.MasterManagement.Domain.Entities.Organization;

namespace A360.MasterManagement.Repository.Repositories;

public interface IOrganizationRepository : IMongoRepository<OrganizationEntity>
{
    Task<OrganizationEntity?> GetByOrganizationCodeAsync(string organizationCode, CancellationToken cancellationToken = default);
}
