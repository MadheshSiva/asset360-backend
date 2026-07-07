using PanelSettingEntity = P360.VisitorManagement.Domain.Entities.VisitorPanelSetting;
using MongoDB.Driver;
using P360.Repository.Repositories;

namespace P360.VisitorManagement.Repository.Repositories;

public sealed class VisitorManagementRepository : MongoRepository<PanelSettingEntity>,
    IVisitorManagementRepository,
    IMongoIndexConfigurator
{
    public const string CollectionName = "visitorpanelsetting";

    public VisitorManagementRepository(IMongoDatabase database)
        : base(database.GetCollection<PanelSettingEntity>(CollectionName))
    {
    }

    public async Task<PanelSettingEntity?> GetByClientIdAsync(
        string clientId,
        CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(x => x.ClientId == clientId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task CreateIndexesAsync(
        CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<PanelSettingEntity>(
                Builders<PanelSettingEntity>.IndexKeys
                    .Ascending(x => x.ClientId),
                new CreateIndexOptions
                {
                    Name = "ix_visitorpanelsetting_client",
                    Unique = true
                })
        };

        await Collection.Indexes.CreateManyAsync(
            indexes,
            cancellationToken);
    }
}
