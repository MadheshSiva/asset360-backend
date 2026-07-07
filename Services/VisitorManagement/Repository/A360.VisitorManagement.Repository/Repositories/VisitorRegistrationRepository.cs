using RegistrationEntity = A360.VisitorManagement.Domain.Entities.VisitorRegistration;
using MongoDB.Driver;
using A360.Repository.Repositories;

namespace A360.VisitorManagement.Repository.Repositories;

public sealed class VisitorRegistrationRepository : MongoRepository<RegistrationEntity>,
    IVisitorRegistrationRepository,
    IMongoIndexConfigurator
{
    public const string CollectionName = "visitorregistration";

    public VisitorRegistrationRepository(IMongoDatabase database)
        : base(database.GetCollection<RegistrationEntity>(CollectionName))
    {
    }

    public async Task<RegistrationEntity?> GetByEmailAsync(
        string email,
        CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(x => x.Email == email)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<RegistrationEntity>> GetByVisitorTypeAsync(
        string visitorType,
        CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(x => x.VisitorType == visitorType)
            .ToListAsync(cancellationToken);
    }

    public async Task CreateIndexesAsync(
        CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<RegistrationEntity>(
                Builders<RegistrationEntity>.IndexKeys
                    .Ascending(x => x.Email),
                new CreateIndexOptions
                {
                    Name = "ix_visitorregistration_email",
                    Unique = true
                }),

            new CreateIndexModel<RegistrationEntity>(
                Builders<RegistrationEntity>.IndexKeys
                    .Ascending(x => x.Status),
                new CreateIndexOptions
                {
                    Name = "ix_visitorregistration_status"
                }),

            new CreateIndexModel<RegistrationEntity>(
                Builders<RegistrationEntity>.IndexKeys
                    .Ascending(x => x.VisitorType),
                new CreateIndexOptions
                {
                    Name = "ix_visitorregistration_visitortype"
                })
        };

        await Collection.Indexes.CreateManyAsync(
            indexes,
            cancellationToken);
    }
}
