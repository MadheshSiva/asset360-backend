using VisitorIdentificationEntity = P360.VisitorManagement.Domain.Entities.VisitorIdentification;
using MongoDB.Driver;
using P360.Repository.Repositories;

namespace P360.VisitorManagement.Repository.Repositories;

public sealed class VisitorIdentificationRepository : MongoRepository<VisitorIdentificationEntity>,
    IVisitorIdentificationRepository,
    IMongoIndexConfigurator
{
    public const string CollectionName = "visitoridentification";

    public VisitorIdentificationRepository(IMongoDatabase database)
        : base(database.GetCollection<VisitorIdentificationEntity>(CollectionName))
    {
    }

    public async Task<IReadOnlyCollection<VisitorIdentificationEntity>> GetByIdentificationTypeAsync(
        string identificationType,
        CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(x => x.IdentificationType == identificationType)
            .ToListAsync(cancellationToken);
    }

    public async Task CreateIndexesAsync(
        CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<VisitorIdentificationEntity>(
                Builders<VisitorIdentificationEntity>.IndexKeys
                    .Ascending(x => x.IdentificationType),
                new CreateIndexOptions
                {
                    Name = "ix_visitoridentification_identification_type"
                })
        };

        await Collection.Indexes.CreateManyAsync(
            indexes,
            cancellationToken);
    }
}
