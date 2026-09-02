using MongoDB.Driver;
using A360.Repository.Repositories;
using PhysicalVerificationResultEntity = A360.MasterManagement.Domain.Entities.PhysicalVerificationResult;

namespace A360.MasterManagement.Repository.Repositories;

public sealed class PhysicalVerificationResultRepository : MongoRepository<PhysicalVerificationResultEntity>, IPhysicalVerificationResultRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "physical_verification_results";

    public PhysicalVerificationResultRepository(IMongoDatabase database)
        : base(database.GetCollection<PhysicalVerificationResultEntity>(CollectionName))
    {
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<PhysicalVerificationResultEntity>(
                Builders<PhysicalVerificationResultEntity>.IndexKeys.Ascending(physicalVerificationResult => physicalVerificationResult.ResultId),
                new CreateIndexOptions { Name = "ix_physical_verification_results_result_id", Unique = true }),
            new CreateIndexModel<PhysicalVerificationResultEntity>(
                Builders<PhysicalVerificationResultEntity>.IndexKeys.Ascending(physicalVerificationResult => physicalVerificationResult.AssetId),
                new CreateIndexOptions { Name = "ix_physical_verification_results_asset_id" }),
            new CreateIndexModel<PhysicalVerificationResultEntity>(
                Builders<PhysicalVerificationResultEntity>.IndexKeys.Ascending(physicalVerificationResult => physicalVerificationResult.ResultCode),
                new CreateIndexOptions { Name = "ix_physical_verification_results_result_code" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
