using EquipmentMasterEntity =
    A360.OTManagement.Domain.Entities.EquipmentMaster;

using MongoDB.Driver;
using A360.Repository.Repositories;

namespace A360.OTManagement.Repository.Repositories;

public sealed class EquipmentMasterRepository
    : MongoRepository<EquipmentMasterEntity>,
      IEquipmentMasterRepository,
      IMongoIndexConfigurator
{
    public const string CollectionName =
        "equipmentmaster";

    public EquipmentMasterRepository(
        IMongoDatabase database)
        : base(
            database.GetCollection<EquipmentMasterEntity>(
                CollectionName))
    {
    }

    public async Task CreateIndexesAsync(
        CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<EquipmentMasterEntity>(
                Builders<EquipmentMasterEntity>.IndexKeys
                    .Ascending(x => x.AssetId),
                new CreateIndexOptions
                {
                    Name = "ix_equipmentmaster_assetid",
                    Unique = true
                }),

            new CreateIndexModel<EquipmentMasterEntity>(
                Builders<EquipmentMasterEntity>.IndexKeys
                    .Ascending(x => x.TagId),
                new CreateIndexOptions
                {
                    Name = "ix_equipmentmaster_tagid",
                    Unique = true
                }),

            new CreateIndexModel<EquipmentMasterEntity>(
                Builders<EquipmentMasterEntity>.IndexKeys
                    .Ascending(x => x.Type)
                    .Ascending(x => x.Location),
                new CreateIndexOptions
                {
                    Name = "ix_equipmentmaster_type_location"
                }),

            new CreateIndexModel<EquipmentMasterEntity>(
                Builders<EquipmentMasterEntity>.IndexKeys
                    .Ascending(x => x.Status),
                new CreateIndexOptions
                {
                    Name = "ix_equipmentmaster_status"
                })
        };

        await Collection.Indexes.CreateManyAsync(
            indexes,
            cancellationToken);
    }
}
