using A360.Repository.Repositories;
using CurrentLocationEntity = A360.MasterManagement.Domain.Entities.CurrentLocation;

namespace A360.MasterManagement.Repository.Repositories;

public interface ICurrentLocationRepository : IMongoRepository<CurrentLocationEntity>
{
}
