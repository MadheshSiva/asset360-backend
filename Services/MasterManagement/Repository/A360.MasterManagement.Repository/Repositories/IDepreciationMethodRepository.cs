using A360.Repository.Repositories;
using DepreciationMethodEntity = A360.MasterManagement.Domain.Entities.DepreciationMethod;

namespace A360.MasterManagement.Repository.Repositories;

public interface IDepreciationMethodRepository : IMongoRepository<DepreciationMethodEntity>
{
}
