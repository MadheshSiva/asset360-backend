using A360.Repository.Repositories;
using AssetTypeFieldEntity = A360.MasterManagement.Domain.Entities.AssetTypeField;

namespace A360.MasterManagement.Repository.Repositories;

public interface IAssetTypeFieldRepository : IMongoRepository<AssetTypeFieldEntity>
{
}
