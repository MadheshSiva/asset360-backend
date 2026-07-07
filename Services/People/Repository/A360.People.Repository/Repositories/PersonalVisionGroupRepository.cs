using PersonalVisionGroupEntity = A360.People.Domain.Entities.PersonalVisionGroup;
using MongoDB.Driver;
using A360.Repository.Repositories;

namespace A360.People.Repository.Repositories;

public sealed class PersonalVisionGroupRepository
    : MongoRepository<PersonalVisionGroupEntity>,
      IPersonalVisionGroupRepository,
      IMongoIndexConfigurator
{
    public const string CollectionName = "personalvisiongroups";

    public PersonalVisionGroupRepository(
        IMongoDatabase database)
        : base(database.GetCollection<PersonalVisionGroupEntity>(
            CollectionName))
    {
    }

    public async Task CreateIndexesAsync(
        CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<PersonalVisionGroupEntity>(
                Builders<PersonalVisionGroupEntity>.IndexKeys
                    .Ascending(x => x.ClientId)
                    .Ascending(x => x.UserId),
                new CreateIndexOptions
                {
                    Name = "ix_pvg_client_user"
                }),

            new CreateIndexModel<PersonalVisionGroupEntity>(
                Builders<PersonalVisionGroupEntity>.IndexKeys
                    .Ascending(x => x.GroupType),
                new CreateIndexOptions
                {
                    Name = "ix_pvg_group_type"
                }),

            new CreateIndexModel<PersonalVisionGroupEntity>(
                Builders<PersonalVisionGroupEntity>.IndexKeys
                    .Ascending(x => x.GroupName),
                new CreateIndexOptions
                {
                    Name = "ix_pvg_group_name"
                })
        };

        await Collection.Indexes.CreateManyAsync(
            indexes,
            cancellationToken);
    }
}