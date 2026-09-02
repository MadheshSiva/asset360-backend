using MongoDB.Driver;
using A360.Repository.Repositories;
using TagEntity = A360.MasterManagement.Domain.Entities.Tag;

namespace A360.MasterManagement.Repository.Repositories;

public sealed class TagRepository : MongoRepository<TagEntity>, ITagRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "tags";

    public TagRepository(IMongoDatabase database)
        : base(database.GetCollection<TagEntity>(CollectionName))
    {
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<TagEntity>(
                Builders<TagEntity>.IndexKeys.Ascending(tag => tag.TagId),
                new CreateIndexOptions { Name = "ix_tags_tag_id", Unique = true }),
            new CreateIndexModel<TagEntity>(
                Builders<TagEntity>.IndexKeys.Ascending(tag => tag.AssetId),
                new CreateIndexOptions { Name = "ix_tags_asset_id" }),
            new CreateIndexModel<TagEntity>(
                Builders<TagEntity>.IndexKeys.Ascending(tag => tag.TagCode),
                new CreateIndexOptions { Name = "ix_tags_tag_code" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
