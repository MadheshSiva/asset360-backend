
using DeviceEntity = P360.Devices.Domain.Entities.Device;
using P360.Repository.Repositories;

namespace P360.Devices.Repository.Repositories;

public interface IDeviceRepository : IMongoRepository<DeviceEntity>
{
    Task<IReadOnlyCollection<DeviceEntity>> GetByTypeAsync(string type, CancellationToken cancellationToken = default);
}
