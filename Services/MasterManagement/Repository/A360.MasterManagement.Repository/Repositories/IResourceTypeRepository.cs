using A360.Repository.Repositories;
using ResourceTypeEntity = A360.MasterManagement.Domain.Entities.ResourceType;

namespace A360.MasterManagement.Repository.Repositories;

public interface IResourceTypeRepository : IMongoRepository<ResourceTypeEntity>
{
}
