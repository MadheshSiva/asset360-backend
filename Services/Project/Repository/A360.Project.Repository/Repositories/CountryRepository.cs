using MongoDB.Driver;
using A360.Repository.Repositories;
using CountryEntity = A360.Project.Domain.Entities.Country;

namespace A360.Project.Repository.Repositories;

public sealed class CountryRepository : MongoRepository<CountryEntity>, ICountryRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "countries";

    public CountryRepository(IMongoDatabase database)
        : base(database.GetCollection<CountryEntity>(CollectionName))
    {
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<CountryEntity>(
                Builders<CountryEntity>.IndexKeys.Ascending(country => country.ProjectId),
                new CreateIndexOptions { Name = "ix_countries_project_id" }),
            new CreateIndexModel<CountryEntity>(
                Builders<CountryEntity>.IndexKeys
                    .Ascending(country => country.ProjectId)
                    .Ascending(country => country.CountryName),
                new CreateIndexOptions { Name = "ix_countries_project_country_name" }),
            new CreateIndexModel<CountryEntity>(
                Builders<CountryEntity>.IndexKeys
                    .Ascending(country => country.ClientId)
                    .Ascending(country => country.Status),
                new CreateIndexOptions { Name = "ix_countries_client_status" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
