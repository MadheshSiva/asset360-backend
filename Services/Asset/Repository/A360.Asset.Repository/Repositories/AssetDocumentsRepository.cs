using MongoDB.Driver;
using A360.Repository.Repositories;
using AssetDocumentsEntity = A360.Asset.Domain.Entities.AssetDocuments;

namespace A360.Asset.Repository.Repositories;

public sealed class AssetDocumentsRepository : MongoRepository<AssetDocumentsEntity>, IAssetDocumentsRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "asset_documents";

    public AssetDocumentsRepository(IMongoDatabase database)
        : base(database.GetCollection<AssetDocumentsEntity>(CollectionName))
    {
    }

    public async Task<AssetDocumentsEntity?> GetByDocumentIdAsync(string documentId, CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(document => document.DocumentId == documentId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<AssetDocumentsEntity>> GetByAssetIdAsync(string assetId, CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(document => document.AssetId == assetId)
            .ToListAsync(cancellationToken);
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<AssetDocumentsEntity>(
                Builders<AssetDocumentsEntity>.IndexKeys.Ascending(document => document.DocumentId),
                new CreateIndexOptions { Name = "ix_asset_documents_document_id", Unique = true }),
            new CreateIndexModel<AssetDocumentsEntity>(
                Builders<AssetDocumentsEntity>.IndexKeys.Ascending(document => document.AssetId),
                new CreateIndexOptions { Name = "ix_asset_documents_asset_id" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
