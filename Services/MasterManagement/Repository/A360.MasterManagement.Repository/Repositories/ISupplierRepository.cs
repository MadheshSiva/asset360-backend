using A360.Repository.Repositories;
using SupplierEntity = A360.MasterManagement.Domain.Entities.Supplier;

namespace A360.MasterManagement.Repository.Repositories;

public interface ISupplierRepository : IMongoRepository<SupplierEntity>
{
    Task<SupplierEntity?> GetBySupplierCodeAsync(string supplierCode, CancellationToken cancellationToken = default);
}
