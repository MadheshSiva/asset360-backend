using ContractorEntity = P360.People.Domain.Entities.Contractor;
using MongoDB.Driver;
using P360.Repository.Repositories;

namespace P360.People.Repository.Repositories;

public sealed class ContractorRepository : MongoRepository<ContractorEntity>,
    IContractorRepository,
    IMongoIndexConfigurator
{
    public const string CollectionName = "contractor";

    public ContractorRepository(IMongoDatabase database)
        : base(database.GetCollection<ContractorEntity>(CollectionName))
    {
    }

    public async Task CreateIndexesAsync(
        CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<ContractorEntity>(
                Builders<ContractorEntity>.IndexKeys
                    .Ascending(x => x.ClientId),
                new CreateIndexOptions
                {
                    Name = "ix_contractor_clientid"
                })
        };

        await Collection.Indexes.CreateManyAsync(
            indexes,
            cancellationToken);
    }
}