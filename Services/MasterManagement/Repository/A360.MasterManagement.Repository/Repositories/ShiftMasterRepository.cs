using MongoDB.Driver;
using A360.Repository.Repositories;
using ShiftMasterEntity = A360.MasterManagement.Domain.Entities.ShiftMaster;

namespace A360.MasterManagement.Repository.Repositories;

public sealed class ShiftMasterRepository : MongoRepository<ShiftMasterEntity>, IShiftMasterRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "shift_masters";

    public ShiftMasterRepository(IMongoDatabase database)
        : base(database.GetCollection<ShiftMasterEntity>(CollectionName))
    {
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<ShiftMasterEntity>(
                Builders<ShiftMasterEntity>.IndexKeys.Ascending(shiftMaster => shiftMaster.ShiftId),
                new CreateIndexOptions { Name = "ix_shift_masters_shift_id", Unique = true }),
            new CreateIndexModel<ShiftMasterEntity>(
                Builders<ShiftMasterEntity>.IndexKeys.Ascending(shiftMaster => shiftMaster.AssetId),
                new CreateIndexOptions { Name = "ix_shift_masters_asset_id" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
