using StaffManagementEntity =
    P360.OTManagement.Domain.Entities.StaffManagement;

using MongoDB.Driver;
using P360.Repository.Repositories;

namespace P360.OTManagement.Repository.Repositories;

public sealed class StaffManagementRepository
    : MongoRepository<StaffManagementEntity>,
      IStaffManagementRepository,
      IMongoIndexConfigurator
{
    public const string CollectionName =
        "staffmanagement";

    public StaffManagementRepository(
        IMongoDatabase database)
        : base(
            database.GetCollection<StaffManagementEntity>(
                CollectionName))
    {
    }

    public async Task CreateIndexesAsync(
        CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<StaffManagementEntity>(
                Builders<StaffManagementEntity>.IndexKeys
                    .Ascending(x => x.StaffId),
                new CreateIndexOptions
                {
                    Name = "ix_staffmanagement_staffid",
                    Unique = true
                }),

            new CreateIndexModel<StaffManagementEntity>(
                Builders<StaffManagementEntity>.IndexKeys
                    .Ascending(x => x.TagId),
                new CreateIndexOptions
                {
                    Name = "ix_staffmanagement_tagid",
                    Unique = true
                }),

            new CreateIndexModel<StaffManagementEntity>(
                Builders<StaffManagementEntity>.IndexKeys
                    .Ascending(x => x.Department)
                    .Ascending(x => x.Role),
                new CreateIndexOptions
                {
                    Name = "ix_staffmanagement_department_role"
                }),

            new CreateIndexModel<StaffManagementEntity>(
                Builders<StaffManagementEntity>.IndexKeys
                    .Ascending(x => x.Shift)
                    .Ascending(x => x.Status),
                new CreateIndexOptions
                {
                    Name = "ix_staffmanagement_shift_status"
                })
        };

        await Collection.Indexes.CreateManyAsync(
            indexes,
            cancellationToken);
    }
}