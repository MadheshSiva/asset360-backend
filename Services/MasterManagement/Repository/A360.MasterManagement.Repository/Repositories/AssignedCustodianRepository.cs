using MongoDB.Driver;
using A360.Repository.Repositories;
using AssignedCustodianEntity = A360.MasterManagement.Domain.Entities.AssignedCustodian;

namespace A360.MasterManagement.Repository.Repositories;

public sealed class AssignedCustodianRepository : MongoRepository<AssignedCustodianEntity>, IAssignedCustodianRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "assigned_custodians";

    public AssignedCustodianRepository(IMongoDatabase database)
        : base(database.GetCollection<AssignedCustodianEntity>(CollectionName))
    {
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<AssignedCustodianEntity>(
                Builders<AssignedCustodianEntity>.IndexKeys.Ascending(assignedCustodian => assignedCustodian.AssignedCustodianId),
                new CreateIndexOptions { Name = "ix_assigned_custodians_assigned_custodian_id", Unique = true }),
            new CreateIndexModel<AssignedCustodianEntity>(
                Builders<AssignedCustodianEntity>.IndexKeys.Ascending(assignedCustodian => assignedCustodian.AssetId),
                new CreateIndexOptions { Name = "ix_assigned_custodians_asset_id" }),
            new CreateIndexModel<AssignedCustodianEntity>(
                Builders<AssignedCustodianEntity>.IndexKeys.Ascending(assignedCustodian => assignedCustodian.CustodianId),
                new CreateIndexOptions { Name = "ix_assigned_custodians_custodian_id" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
