using A360.Repository.Repositories;
using BusinessUnitEntity = A360.MasterManagement.Domain.Entities.BusinessUnit;

namespace A360.MasterManagement.Repository.Repositories;

public interface IBusinessUnitRepository : IMongoRepository<BusinessUnitEntity>
{
    Task<BusinessUnitEntity?> GetByBusinessUnitCodeAsync(string businessUnitCode, CancellationToken cancellationToken = default);
}
