using StaffManagementEntity =
    P360.OTManagement.Domain.Entities.StaffManagement;

using P360.Repository.Repositories;

namespace P360.OTManagement.Repository.Repositories;

public interface IStaffManagementRepository
    : IMongoRepository<StaffManagementEntity>
{
}