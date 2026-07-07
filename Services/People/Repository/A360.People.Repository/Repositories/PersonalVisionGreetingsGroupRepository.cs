using GreetingsGroupsEntity =
    A360.People.Domain.Entities.PersonalVisionGreetingsGroups;

using MongoDB.Driver;
using A360.Repository.Repositories;

namespace A360.People.Repository.Repositories;

public sealed class PersonalVisionGreetingsGroupsRepository
    : MongoRepository<GreetingsGroupsEntity>,
      IPersonalVisionGreetingsGroupsRepository,
      IMongoIndexConfigurator
{
    public const string CollectionName =
        "personalvisiongreetingsgroups";

    public PersonalVisionGreetingsGroupsRepository(
        IMongoDatabase database)
        : base(database.GetCollection<GreetingsGroupsEntity>(
            CollectionName))
    {
    }

    public async Task CreateIndexesAsync(
        CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<GreetingsGroupsEntity>(
                Builders<GreetingsGroupsEntity>.IndexKeys
                    .Ascending(x => x.GroupType)),

            new CreateIndexModel<GreetingsGroupsEntity>(
                Builders<GreetingsGroupsEntity>.IndexKeys
                    .Ascending(x => x.GroupName)),

            new CreateIndexModel<GreetingsGroupsEntity>(
                Builders<GreetingsGroupsEntity>.IndexKeys
                    .Ascending(x => x.GreetingsType)),

            new CreateIndexModel<GreetingsGroupsEntity>(
                Builders<GreetingsGroupsEntity>.IndexKeys
                    .Ascending(x => x.Status))
        };

        await Collection.Indexes.CreateManyAsync(
            indexes,
            cancellationToken);
    }
}