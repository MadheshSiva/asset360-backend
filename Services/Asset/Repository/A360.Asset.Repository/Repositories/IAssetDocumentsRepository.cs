using A360.Repository.Repositories;
using AssetDocumentsEntity = A360.Asset.Domain.Entities.AssetDocuments;

namespace A360.Asset.Repository.Repositories;

public interface IAssetDocumentsRepository : IMongoRepository<AssetDocumentsEntity>
{
    Task<AssetDocumentsEntity?> GetByDocumentIdAsync(string documentId, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<AssetDocumentsEntity>> GetByAssetIdAsync(string assetId, CancellationToken cancellationToken = default);
}
