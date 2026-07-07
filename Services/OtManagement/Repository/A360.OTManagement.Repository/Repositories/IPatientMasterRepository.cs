using PatientMasterEntity =
    A360.OTManagement.Domain.Entities.PatientMaster;

using A360.Repository.Repositories;

namespace A360.OTManagement.Repository.Repositories;

public interface IPatientMasterRepository
    : IMongoRepository<PatientMasterEntity>
{
}