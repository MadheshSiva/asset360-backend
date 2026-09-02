using A360.Repository.Repositories;
using AssignedCustodianEntity = A360.MasterManagement.Domain.Entities.AssignedCustodian;

namespace A360.MasterManagement.Repository.Repositories;

public interface IAssignedCustodianRepository : IMongoRepository<AssignedCustodianEntity>
{
}
