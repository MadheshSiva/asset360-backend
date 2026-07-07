using EmployeeEntity = A360.People.Domain.Entities.Employee;
using MongoDB.Driver;
using A360.Repository.Repositories;

namespace A360.People.Repository.Repositories;

public sealed class EmployeeRepository : MongoRepository<EmployeeEntity>,
    IEmployeeRepository,
    IMongoIndexConfigurator
{
    public const string CollectionName = "employee";

    public EmployeeRepository(IMongoDatabase database)
        : base(database.GetCollection<EmployeeEntity>(CollectionName))
    {
    }

    public async Task CreateIndexesAsync(
        CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<EmployeeEntity>(
                Builders<EmployeeEntity>.IndexKeys
                    .Ascending(x => x.ClientId)
                    .Ascending(x => x.IDNumber),
                new CreateIndexOptions
                {
                    Name = "ix_employee_client_employeeid"
                }),

            new CreateIndexModel<EmployeeEntity>(
                Builders<EmployeeEntity>.IndexKeys
                    .Ascending(x => x.Dept)
                    .Ascending(x => x.Role),
                new CreateIndexOptions
                {
                    Name = "ix_employee_department_role"
                }),

            new CreateIndexModel<EmployeeEntity>(
                Builders<EmployeeEntity>.IndexKeys
                    .Ascending(x => x.Company),
                new CreateIndexOptions
                {
                    Name = "ix_employee_company"
                })
        };

        await Collection.Indexes.CreateManyAsync(
            indexes,
            cancellationToken);
    }
}