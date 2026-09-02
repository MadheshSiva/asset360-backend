using MongoDB.Driver;
using A360.Repository.Repositories;
using CategoryEntity = A360.MasterManagement.Domain.Entities.Category;

namespace A360.MasterManagement.Repository.Repositories;

public sealed class CategoryRepository : MongoRepository<CategoryEntity>, ICategoryRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "categories";

    public CategoryRepository(IMongoDatabase database)
        : base(database.GetCollection<CategoryEntity>(CollectionName))
    {
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<CategoryEntity>(
                Builders<CategoryEntity>.IndexKeys.Ascending(category => category.CategoryId),
                new CreateIndexOptions { Name = "ix_categories_category_id", Unique = true }),
            new CreateIndexModel<CategoryEntity>(
                Builders<CategoryEntity>.IndexKeys.Ascending(category => category.AssetId),
                new CreateIndexOptions { Name = "ix_categories_asset_id" }),
            new CreateIndexModel<CategoryEntity>(
                Builders<CategoryEntity>.IndexKeys.Ascending(category => category.CategoryCode),
                new CreateIndexOptions { Name = "ix_categories_category_code" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
