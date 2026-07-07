using AccessEntity = A360.People.Domain.Entities.Access;
using MongoDB.Driver;
using A360.Repository.Repositories;

namespace A360.People.Repository.Repositories;

public sealed class AccessRepository : MongoRepository<AccessEntity>,
    IAccessRepository,
    IMongoIndexConfigurator
{
    public const string CollectionName = "access";

    public AccessRepository(IMongoDatabase database)
        : base(database.GetCollection<AccessEntity>(CollectionName))
    {
    }

    public async Task CreateIndexesAsync(
        CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<AccessEntity>(
                Builders<AccessEntity>.IndexKeys
                    .Ascending(x => x.ClientId)
                    .Ascending(x => x.GroupName),
                new CreateIndexOptions
                {
                    Name = "ix_access_client_groupname"
                }),

            new CreateIndexModel<AccessEntity>(
                Builders<AccessEntity>.IndexKeys
                    .Ascending(x => x.GroupType),
                new CreateIndexOptions
                {
                    Name = "ix_access_group_type"
                }),

            new CreateIndexModel<AccessEntity>(
                Builders<AccessEntity>.IndexKeys
                    .Ascending(x => x.FromDateTime)
                    .Ascending(x => x.ToDateTime),
                new CreateIndexOptions
                {
                    Name = "ix_access_schedule"
                }),

            new CreateIndexModel<AccessEntity>(
                Builders<AccessEntity>.IndexKeys
                    .Ascending(x => x.Status),
                new CreateIndexOptions
                {
                    Name = "ix_access_status"
                })
        };

        await Collection.Indexes.CreateManyAsync(
            indexes,
            cancellationToken);
    }
}