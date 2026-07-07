using GatePassEntity = A360.VisitorManagement.Domain.Entities.VisitorGatePass;
using MongoDB.Driver;
using A360.Repository.Repositories;

namespace A360.VisitorManagement.Repository.Repositories;

public sealed class VisitorGatePassRepository : MongoRepository<GatePassEntity>,
    IVisitorGatePassRepository,
    IMongoIndexConfigurator
{
    public const string CollectionName = "visitorgatepass";

    public VisitorGatePassRepository(IMongoDatabase database)
        : base(database.GetCollection<GatePassEntity>(CollectionName))
    {
    }

    public async Task CreateIndexesAsync(
        CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<GatePassEntity>(
                Builders<GatePassEntity>.IndexKeys
                    .Ascending(x => x.Status),
                new CreateIndexOptions
                {
                    Name = "ix_visitorgatepass_status"
                }),

            new CreateIndexModel<GatePassEntity>(
                Builders<GatePassEntity>.IndexKeys
                    .Ascending(x => x.AuthCode),
                new CreateIndexOptions
                {
                    Name = "ix_visitorgatepass_authcode"
                }),

            new CreateIndexModel<GatePassEntity>(
                Builders<GatePassEntity>.IndexKeys
                    .Ascending(x => x.ClientId),
                new CreateIndexOptions
                {
                    Name = "ix_visitorgatepass_client_id"
                })
        };

        await Collection.Indexes.CreateManyAsync(
            indexes,
            cancellationToken);
    }
}
