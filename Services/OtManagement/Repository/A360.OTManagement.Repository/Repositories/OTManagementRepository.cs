using OTManagementEntity =
    A360.OTManagement.Domain.Entities.OTManagement;

using MongoDB.Driver;
using A360.Repository.Repositories;

namespace A360.OTManagement.Repository.Repositories;

public sealed class OTManagementRepository
    : MongoRepository<OTManagementEntity>,
      IOTManagementRepository,
      IMongoIndexConfigurator
{
    public const string CollectionName =
        "otmanagement";

    public OTManagementRepository(
        IMongoDatabase database)
        : base(
            database.GetCollection<OTManagementEntity>(
                CollectionName))
    {
    }

    public async Task CreateIndexesAsync(
        CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<OTManagementEntity>(
                Builders<OTManagementEntity>.IndexKeys
                    .Ascending(x => x.UniqueId),
                new CreateIndexOptions
                {
                    Name = "ix_otmanagement_uniqueid",
                    Unique = true
                }),

            new CreateIndexModel<OTManagementEntity>(
                Builders<OTManagementEntity>.IndexKeys
                    .Ascending(x => x.OTName),
                new CreateIndexOptions
                {
                    Name = "ix_otmanagement_name"
                }),

            new CreateIndexModel<OTManagementEntity>(
                Builders<OTManagementEntity>.IndexKeys
                    .Ascending(x => x.Department)
                    .Ascending(x => x.Floor),
                new CreateIndexOptions
                {
                    Name = "ix_otmanagement_department_floor"
                }),

            new CreateIndexModel<OTManagementEntity>(
                Builders<OTManagementEntity>.IndexKeys
                    .Ascending(x => x.Project)
                    .Ascending(x => x.Country)
                    .Ascending(x => x.Area),
                new CreateIndexOptions
                {
                    Name = "ix_otmanagement_location"
                })
        };

        await Collection.Indexes.CreateManyAsync(
            indexes,
            cancellationToken);
    }
}