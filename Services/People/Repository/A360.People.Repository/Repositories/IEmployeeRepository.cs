using EmployeeEntity = A360.People.Domain.Entities.Employee;
using A360.Repository.Repositories;

namespace A360.People.Repository.Repositories;

public interface IEmployeeRepository : IMongoRepository<EmployeeEntity>
{
}