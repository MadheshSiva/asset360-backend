using A360.Repository.Repositories;
using DepartmentEntity = A360.MasterManagement.Domain.Entities.Department;

namespace A360.MasterManagement.Repository.Repositories;

public interface IDepartmentRepository : IMongoRepository<DepartmentEntity>
{
    Task<DepartmentEntity?> GetByDepartmentCodeAsync(string departmentCode, CancellationToken cancellationToken = default);
}
