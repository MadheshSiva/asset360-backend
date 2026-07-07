using EmployeeEntity = P360.People.Domain.Entities.Employee;
using P360.Repository.Repositories;

namespace P360.People.Repository.Repositories;

public interface IEmployeeRepository : IMongoRepository<EmployeeEntity>
{
}