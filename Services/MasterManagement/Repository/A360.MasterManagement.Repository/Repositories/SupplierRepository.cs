using MongoDB.Driver;
using A360.Repository.Repositories;
using SupplierEntity = A360.MasterManagement.Domain.Entities.Supplier;

namespace A360.MasterManagement.Repository.Repositories;

public sealed class SupplierRepository : MongoRepository<SupplierEntity>, ISupplierRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "suppliers";

    public SupplierRepository(IMongoDatabase database)
        : base(database.GetCollection<SupplierEntity>(CollectionName))
    {
    }

    public async Task<SupplierEntity?> GetBySupplierCodeAsync(string supplierCode, CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(supplier => supplier.SupplierCode == supplierCode)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<SupplierEntity>(
                Builders<SupplierEntity>.IndexKeys.Ascending(supplier => supplier.SupplierCode),
                new CreateIndexOptions { Name = "ix_suppliers_supplier_code", Unique = true }),
            new CreateIndexModel<SupplierEntity>(
                Builders<SupplierEntity>.IndexKeys.Ascending(supplier => supplier.AssetId),
                new CreateIndexOptions { Name = "ix_suppliers_asset_id" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
