using A360.Repository.Repositories;
using AlertTypeEntity = A360.MasterManagement.Domain.Entities.AlertType;

namespace A360.MasterManagement.Repository.Repositories;

public interface IAlertTypeRepository : IMongoRepository<AlertTypeEntity>
{
}
