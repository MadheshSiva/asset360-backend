using ApprovalEntity = A360.VisitorManagement.Domain.Entities.VisitorApproval;
using MongoDB.Driver;
using A360.Repository.Repositories;

namespace A360.VisitorManagement.Repository.Repositories;

public sealed class VisitorApprovalRepository : MongoRepository<ApprovalEntity>,
    IVisitorApprovalRepository,
    IMongoIndexConfigurator
{
    public const string CollectionName = "visitorapproval";

    public VisitorApprovalRepository(IMongoDatabase database)
        : base(database.GetCollection<ApprovalEntity>(CollectionName))
    {
    }

    public async Task<ApprovalEntity?> GetByPermitTypeAsync(
        string permitType,
        CancellationToken cancellationToken = default)
    {
        var filter = Builders<ApprovalEntity>.Filter.Eq(x => x.PermitType, permitType);

        return await Collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task CreateIndexesAsync(
        CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<ApprovalEntity>(
                Builders<ApprovalEntity>.IndexKeys
                    .Ascending(x => x.PermitType),
                new CreateIndexOptions
                {
                    Name = "ix_visitorapproval_permittype"
                })
        };

        await Collection.Indexes.CreateManyAsync(
            indexes,
            cancellationToken);
    }
}
