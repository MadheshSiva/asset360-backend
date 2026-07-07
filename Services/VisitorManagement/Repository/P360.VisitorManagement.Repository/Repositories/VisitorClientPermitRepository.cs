using ClientPermitEntity = P360.VisitorManagement.Domain.Entities.VisitorClientPermit;
using MongoDB.Driver;
using P360.Repository.Repositories;

namespace P360.VisitorManagement.Repository.Repositories;

public sealed class VisitorClientPermitRepository : MongoRepository<ClientPermitEntity>,
    IVisitorClientPermitRepository,
    IMongoIndexConfigurator
{
    public const string CollectionName = "clientpermit";

    public VisitorClientPermitRepository(IMongoDatabase database)
        : base(database.GetCollection<ClientPermitEntity>(CollectionName))
    {
    }

    public async Task CreateIndexesAsync(
        CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<ClientPermitEntity>(
                Builders<ClientPermitEntity>.IndexKeys
                    .Ascending(x => x.ClientEmail),
                new CreateIndexOptions
                {
                    Name = "ix_clientpermit_client_email"
                })
        };

        await Collection.Indexes.CreateManyAsync(
            indexes,
            cancellationToken);
    }
}
