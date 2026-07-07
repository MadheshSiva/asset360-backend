using EmailTemplateEntity = P360.VisitorManagement.Domain.Entities.EmailTemplate;
using MongoDB.Driver;
using P360.Repository.Repositories;

namespace P360.VisitorManagement.Repository.Repositories;

public sealed class EmailTemplateRepository : MongoRepository<EmailTemplateEntity>,
    IEmailTemplateRepository,
    IMongoIndexConfigurator
{
    public const string CollectionName = "emailtemplates";

    public EmailTemplateRepository(IMongoDatabase database)
        : base(database.GetCollection<EmailTemplateEntity>(CollectionName))
    {
    }

    public async Task<EmailTemplateEntity?> GetByNameAsync(
        string name,
        CancellationToken cancellationToken = default)
    {
        var filter = Builders<EmailTemplateEntity>.Filter.Eq(x => x.Name, name);

        return await Collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task CreateIndexesAsync(
        CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<EmailTemplateEntity>(
                Builders<EmailTemplateEntity>.IndexKeys
                    .Ascending(x => x.Name),
                new CreateIndexOptions
                {
                    Name = "ix_emailtemplates_name",
                    Unique = true
                })
        };

        await Collection.Indexes.CreateManyAsync(
            indexes,
            cancellationToken);
    }
}
