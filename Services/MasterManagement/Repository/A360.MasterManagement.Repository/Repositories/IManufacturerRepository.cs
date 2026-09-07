using A360.Repository.Repositories;
using ManufacturerEntity = A360.MasterManagement.Domain.Entities.Manufacturer;

namespace A360.MasterManagement.Repository.Repositories;

public interface IManufacturerRepository : IMongoRepository<ManufacturerEntity>
{
    Task<ManufacturerEntity?> GetByManufacturerCodeAsync(string manufacturerCode, CancellationToken cancellationToken = default);
}
