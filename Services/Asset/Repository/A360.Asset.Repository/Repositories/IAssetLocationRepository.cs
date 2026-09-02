using A360.Repository.Repositories;
using AssetLocationEntity = A360.Asset.Domain.Entities.AssetLocation;

namespace A360.Asset.Repository.Repositories;

public interface IAssetLocationRepository : IMongoRepository<AssetLocationEntity>
{
}
