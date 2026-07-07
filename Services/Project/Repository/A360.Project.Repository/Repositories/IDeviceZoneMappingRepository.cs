using A360.Repository.Repositories;
using DeviceZoneMappingEntity = A360.Project.Domain.Entities.DeviceZoneMapping;

namespace A360.Project.Repository.Repositories;

public interface IDeviceZoneMappingRepository : IMongoRepository<DeviceZoneMappingEntity>
{
}
