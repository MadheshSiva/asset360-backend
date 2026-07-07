using PatientMasterEntity =
    P360.OTManagement.Domain.Entities.PatientMaster;

using P360.Repository.Repositories;

namespace P360.OTManagement.Repository.Repositories;

public interface IPatientMasterRepository
    : IMongoRepository<PatientMasterEntity>
{
}