using VisitorEntity = A360.People.Domain.Entities.Visitor;
using MongoDB.Driver;
using A360.Repository.Repositories;

namespace A360.People.Repository.Repositories;

public sealed class VisitorRepository : MongoRepository<VisitorEntity>,
    IVisitorRepository,
    IMongoIndexConfigurator
{
    public const string CollectionName = "visitor";

    public VisitorRepository(IMongoDatabase database)
        : base(database.GetCollection<VisitorEntity>(CollectionName))
    {
    }

    public async Task<IReadOnlyCollection<VisitorEntity>> GetByEmailAsync(
        string clientId,
        string email,
        CancellationToken cancellationToken = default)
    {
        var filter = Builders<VisitorEntity>.Filter.And(
            Builders<VisitorEntity>.Filter.Eq(x => x.ClientId, clientId),
            Builders<VisitorEntity>.Filter.Eq(x => x.Email, email));

        return await Collection.Find(filter).ToListAsync(cancellationToken);
    }

    public async Task<VisitorEntity?> GetByAuthCodeAsync(
        string authCode,
        CancellationToken cancellationToken = default)
    {
        var filter = Builders<VisitorEntity>.Filter.Eq(x => x.AuthCode, authCode);

        return await Collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task CreateIndexesAsync(
        CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<VisitorEntity>(
                Builders<VisitorEntity>.IndexKeys
                    .Ascending(x => x.ClientId)
                    .Ascending(x => x.IDNumber),
                new CreateIndexOptions
                {
                    Name = "ix_visitor_client_visitorid"
                }),

            new CreateIndexModel<VisitorEntity>(
                Builders<VisitorEntity>.IndexKeys
                    .Ascending(x => x.Dept),
                new CreateIndexOptions
                {
                    Name = "ix_visitor_department"
                }),

            new CreateIndexModel<VisitorEntity>(
                Builders<VisitorEntity>.IndexKeys
                    .Ascending(x => x.Company),
                new CreateIndexOptions
                {
                    Name = "ix_visitor_company"
                }),

            new CreateIndexModel<VisitorEntity>(
                Builders<VisitorEntity>.IndexKeys
                    .Ascending(x => x.Email),
                new CreateIndexOptions
                {
                    Name = "ix_visitor_email"
                }),

            new CreateIndexModel<VisitorEntity>(
                Builders<VisitorEntity>.IndexKeys
                    .Ascending(x => x.ClientId)
                    .Ascending(x => x.Email),
                new CreateIndexOptions
                {
                    Name = "ix_visitor_client_email"
                }),

            new CreateIndexModel<VisitorEntity>(
                Builders<VisitorEntity>.IndexKeys
                    .Ascending(x => x.AuthCode),
                new CreateIndexOptions
                {
                    Name = "ix_visitor_authcode"
                })
        };

        await Collection.Indexes.CreateManyAsync(
            indexes,
            cancellationToken);
    }
}