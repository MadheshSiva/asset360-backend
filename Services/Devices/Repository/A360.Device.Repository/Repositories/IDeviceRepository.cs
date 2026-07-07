
using DeviceEntity = A360.Devices.Domain.Entities.Device;
using A360.Repository.Repositories;

namespace A360.Devices.Repository.Repositories;

public interface IDeviceRepository : IMongoRepository<DeviceEntity>
{
    Task<IReadOnlyCollection<DeviceEntity>> GetByTypeAsync(string type, CancellationToken cancellationToken = default);
}
