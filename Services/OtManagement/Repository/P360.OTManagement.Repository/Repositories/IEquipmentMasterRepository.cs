using EquipmentMasterEntity =
    P360.OTManagement.Domain.Entities.EquipmentMaster;

using P360.Repository.Repositories;

namespace P360.OTManagement.Repository.Repositories;

public interface IEquipmentMasterRepository
    : IMongoRepository<EquipmentMasterEntity>
{
}
