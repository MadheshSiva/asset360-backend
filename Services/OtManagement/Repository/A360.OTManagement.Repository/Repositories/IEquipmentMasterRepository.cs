using EquipmentMasterEntity =
    A360.OTManagement.Domain.Entities.EquipmentMaster;

using A360.Repository.Repositories;

namespace A360.OTManagement.Repository.Repositories;

public interface IEquipmentMasterRepository
    : IMongoRepository<EquipmentMasterEntity>
{
}
