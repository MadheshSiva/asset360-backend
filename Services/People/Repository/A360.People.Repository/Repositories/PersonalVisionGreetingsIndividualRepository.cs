using GreetingsEntity = A360.People.Domain.Entities.PersonalVisionGreetingsIndividual;
using MongoDB.Driver;
using A360.Repository.Repositories;

namespace A360.People.Repository.Repositories;

public sealed class PersonalVisionGreetingsIndividualRepository
    : MongoRepository<GreetingsEntity>,
      IPersonalVisionGreetingsIndividualRepository,
      IMongoIndexConfigurator
{
    public const string CollectionName =
        "personalvisiongreetingsindividual";

    public PersonalVisionGreetingsIndividualRepository(
        IMongoDatabase database)
        : base(database.GetCollection<GreetingsEntity>(
            CollectionName))
    {
    }

    public async Task CreateIndexesAsync(
        CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<GreetingsEntity>(
                Builders<GreetingsEntity>.IndexKeys
                    .Ascending(x => x.GreetingsType)),

            new CreateIndexModel<GreetingsEntity>(
                Builders<GreetingsEntity>.IndexKeys
                    .Ascending(x => x.Status))
        };

        await Collection.Indexes.CreateManyAsync(
            indexes,
            cancellationToken);
    }
}