using StaffManagementEntity =
    A360.OTManagement.Domain.Entities.StaffManagement;

using A360.Repository.Repositories;

namespace A360.OTManagement.Repository.Repositories;

public interface IStaffManagementRepository
    : IMongoRepository<StaffManagementEntity>
{
}