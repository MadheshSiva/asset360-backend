using A360.Repository.Repositories;
using TaggedAssetsEntity = A360.Asset.Domain.Entities.TaggedAssets;

namespace A360.Asset.Repository.Repositories;

public interface ITaggedAssetsRepository : IMongoRepository<TaggedAssetsEntity>
{
    Task<TaggedAssetsEntity?> GetByTaggedAssetIdAsync(string taggedAssetId, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<TaggedAssetsEntity>> GetByAssetIdAsync(string assetId, CancellationToken cancellationToken = default);
}
