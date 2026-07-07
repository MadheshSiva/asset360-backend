using GroupEntity = A360.People.Domain.Entities.Group;
using MongoDB.Driver;
using A360.Repository.Repositories;

namespace A360.People.Repository.Repositories;

public sealed class GroupRepository : MongoRepository<GroupEntity>,
    IGroupRepository,
    IMongoIndexConfigurator
{
    public const string CollectionName = "groups";

    public GroupRepository(IMongoDatabase database)
        : base(database.GetCollection<GroupEntity>(CollectionName))
    {
    }

    public async Task CreateIndexesAsync(
        CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<GroupEntity>(
                Builders<GroupEntity>.IndexKeys
                    .Ascending(x => x.ClientId)
                    .Ascending(x => x.GroupType),
                new CreateIndexOptions
                {
                    Name = "ix_group_client_group_type"
                }),

            new CreateIndexModel<GroupEntity>(
                Builders<GroupEntity>.IndexKeys
                    .Ascending(x => x.GroupName),
                new CreateIndexOptions
                {
                    Name = "ix_group_name"
                })
        };

        await Collection.Indexes.CreateManyAsync(
            indexes,
            cancellationToken);
    }

    public async Task<IEnumerable<GroupEntity>> GetByGroupTypeAsync(
    string groupType,
    CancellationToken cancellationToken = default)
{
    var filter = Builders<GroupEntity>.Filter.Eq(
        x => x.GroupType,
        groupType);

    return await Collection
        .Find(filter)
        .ToListAsync(cancellationToken);
}


    public async Task<GroupEntity?> GetByIdAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

}