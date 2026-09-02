using MongoDB.Driver;
using A360.Repository.Repositories;
using SkillMasterEntity = A360.MasterManagement.Domain.Entities.SkillMaster;

namespace A360.MasterManagement.Repository.Repositories;

public sealed class SkillMasterRepository : MongoRepository<SkillMasterEntity>, ISkillMasterRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "skill_masters";

    public SkillMasterRepository(IMongoDatabase database)
        : base(database.GetCollection<SkillMasterEntity>(CollectionName))
    {
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<SkillMasterEntity>(
                Builders<SkillMasterEntity>.IndexKeys.Ascending(skillMaster => skillMaster.SkillId),
                new CreateIndexOptions { Name = "ix_skill_masters_skill_id", Unique = true }),
            new CreateIndexModel<SkillMasterEntity>(
                Builders<SkillMasterEntity>.IndexKeys.Ascending(skillMaster => skillMaster.AssetId),
                new CreateIndexOptions { Name = "ix_skill_masters_asset_id" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
