using ReconcilePassEntity = P360.VisitorManagement.Domain.Entities.VisitorReconcilePass;
using MongoDB.Driver;
using P360.Repository.Repositories;

namespace P360.VisitorManagement.Repository.Repositories;

public sealed class VisitorReconcilePassRepository : MongoRepository<ReconcilePassEntity>,
    IVisitorReconcilePassRepository,
    IMongoIndexConfigurator
{
    public const string CollectionName = "reconcilepass";

    public VisitorReconcilePassRepository(IMongoDatabase database)
        : base(database.GetCollection<ReconcilePassEntity>(CollectionName))
    {
    }

    public async Task CreateIndexesAsync(
        CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<ReconcilePassEntity>(
                Builders<ReconcilePassEntity>.IndexKeys
                    .Ascending(x => x.CreatedBy),
                new CreateIndexOptions
                {
                    Name = "ix_reconcilepass_created_by"
                })
        };

        await Collection.Indexes.CreateManyAsync(
            indexes,
            cancellationToken);
    }
}
