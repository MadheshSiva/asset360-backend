using MongoDB.Driver;
using A360.Repository.Repositories;
using DepartmentEntity = A360.MasterManagement.Domain.Entities.Department;

namespace A360.MasterManagement.Repository.Repositories;

public sealed class DepartmentRepository : MongoRepository<DepartmentEntity>, IDepartmentRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "departments";

    public DepartmentRepository(IMongoDatabase database)
        : base(database.GetCollection<DepartmentEntity>(CollectionName))
    {
    }

    public async Task<DepartmentEntity?> GetByDepartmentCodeAsync(string departmentCode, CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(department => department.DepartmentCode == departmentCode)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<DepartmentEntity>(
                Builders<DepartmentEntity>.IndexKeys.Ascending(department => department.DepartmentCode),
                new CreateIndexOptions { Name = "ix_departments_department_code", Unique = true }),
            new CreateIndexModel<DepartmentEntity>(
                Builders<DepartmentEntity>.IndexKeys.Ascending(department => department.AssetId),
                new CreateIndexOptions { Name = "ix_departments_asset_id" }),
            new CreateIndexModel<DepartmentEntity>(
                Builders<DepartmentEntity>.IndexKeys.Ascending(department => department.BusinessUnit),
                new CreateIndexOptions { Name = "ix_departments_business_unit" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
