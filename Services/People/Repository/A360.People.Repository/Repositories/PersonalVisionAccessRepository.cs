using AccessEntity = A360.People.Domain.Entities.PersonalVisionAccess;
using MongoDB.Driver;
using A360.Repository.Repositories;

namespace A360.People.Repository.Repositories;

public sealed class PersonalVisionAccessRepository
    : MongoRepository<AccessEntity>,
      IPersonalVisionAccessRepository,
      IMongoIndexConfigurator
{
    public const string CollectionName = "personalvisionaccess";

    public PersonalVisionAccessRepository(
        IMongoDatabase database)
        : base(database.GetCollection<AccessEntity>(
            CollectionName))
    {
    }

    public async Task CreateIndexesAsync(
        CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<AccessEntity>(
                Builders<AccessEntity>.IndexKeys
                    .Ascending(x => x.GroupName)
                    .Ascending(x => x.GroupType),
                new CreateIndexOptions
                {
                    Name = "ix_pva_groupname_grouptype"
                }),

            new CreateIndexModel<AccessEntity>(
                Builders<AccessEntity>.IndexKeys
                    .Ascending(x => x.Status),
                new CreateIndexOptions
                {
                    Name = "ix_pva_status"
                }),

            new CreateIndexModel<AccessEntity>(
                Builders<AccessEntity>.IndexKeys
                    .Ascending(x => x.CreatedBy),
                new CreateIndexOptions
                {
                    Name = "ix_pva_createdby"
                })
        };

        await Collection.Indexes.CreateManyAsync(
            indexes,
            cancellationToken);
    }
}