using EntryExitEntity = A360.VisitorManagement.Domain.Entities.VisitorEntryExit;
using MongoDB.Driver;
using A360.Repository.Repositories;

namespace A360.VisitorManagement.Repository.Repositories;

public sealed class VisitorEntryExitRepository : MongoRepository<EntryExitEntity>,
    IVisitorEntryExitRepository,
    IMongoIndexConfigurator
{
    public const string CollectionName = "visitorentryexit";

    public VisitorEntryExitRepository(IMongoDatabase database)
        : base(database.GetCollection<EntryExitEntity>(CollectionName))
    {
    }

    public async Task CreateIndexesAsync(
        CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<EntryExitEntity>(
                Builders<EntryExitEntity>.IndexKeys
                    .Ascending(x => x.Type),
                new CreateIndexOptions
                {
                    Name = "ix_visitorentryexit_type"
                })
        };

        await Collection.Indexes.CreateManyAsync(
            indexes,
            cancellationToken);
    }
}
