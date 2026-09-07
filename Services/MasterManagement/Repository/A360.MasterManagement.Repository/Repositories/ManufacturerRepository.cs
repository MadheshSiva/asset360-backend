using MongoDB.Driver;
using A360.Repository.Repositories;
using ManufacturerEntity = A360.MasterManagement.Domain.Entities.Manufacturer;

namespace A360.MasterManagement.Repository.Repositories;

public sealed class ManufacturerRepository : MongoRepository<ManufacturerEntity>, IManufacturerRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "manufacturers";

    public ManufacturerRepository(IMongoDatabase database)
        : base(database.GetCollection<ManufacturerEntity>(CollectionName))
    {
    }

    public async Task<ManufacturerEntity?> GetByManufacturerCodeAsync(string manufacturerCode, CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(manufacturer => manufacturer.ManufacturerCode == manufacturerCode)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<ManufacturerEntity>(
                Builders<ManufacturerEntity>.IndexKeys.Ascending(manufacturer => manufacturer.ManufacturerCode),
                new CreateIndexOptions { Name = "ix_manufacturers_manufacturer_code", Unique = true }),
            new CreateIndexModel<ManufacturerEntity>(
                Builders<ManufacturerEntity>.IndexKeys.Ascending(manufacturer => manufacturer.AssetId),
                new CreateIndexOptions { Name = "ix_manufacturers_asset_id" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
