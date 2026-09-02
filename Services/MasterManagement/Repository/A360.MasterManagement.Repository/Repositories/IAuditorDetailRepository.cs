using A360.Repository.Repositories;
using AuditorDetailEntity = A360.MasterManagement.Domain.Entities.AuditorDetail;

namespace A360.MasterManagement.Repository.Repositories;

public interface IAuditorDetailRepository : IMongoRepository<AuditorDetailEntity>
{
}
