using P360.Repository.Repositories;
using DeviceZoneMappingEntity = P360.Project.Domain.Entities.DeviceZoneMapping;

namespace P360.Project.Repository.Repositories;

public interface IDeviceZoneMappingRepository : IMongoRepository<DeviceZoneMappingEntity>
{
}
