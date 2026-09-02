using MongoDB.Driver;
using A360.Repository.Repositories;
using IssueTypeMasterEntity = A360.MasterManagement.Domain.Entities.IssueTypeMaster;

namespace A360.MasterManagement.Repository.Repositories;

public sealed class IssueTypeMasterRepository : MongoRepository<IssueTypeMasterEntity>, IIssueTypeMasterRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "issue_type_masters";

    public IssueTypeMasterRepository(IMongoDatabase database)
        : base(database.GetCollection<IssueTypeMasterEntity>(CollectionName))
    {
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<IssueTypeMasterEntity>(
                Builders<IssueTypeMasterEntity>.IndexKeys.Ascending(issueTypeMaster => issueTypeMaster.IssueTypeId),
                new CreateIndexOptions { Name = "ix_issue_type_masters_issue_type_id", Unique = true }),
            new CreateIndexModel<IssueTypeMasterEntity>(
                Builders<IssueTypeMasterEntity>.IndexKeys.Ascending(issueTypeMaster => issueTypeMaster.AssetId),
                new CreateIndexOptions { Name = "ix_issue_type_masters_asset_id" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
