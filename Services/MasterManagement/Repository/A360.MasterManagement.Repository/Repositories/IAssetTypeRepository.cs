using A360.Repository.Repositories;
using AssetTypeEntity = A360.MasterManagement.Domain.Entities.AssetType;

namespace A360.MasterManagement.Repository.Repositories;

public interface IAssetTypeRepository : IMongoRepository<AssetTypeEntity>
{
}
