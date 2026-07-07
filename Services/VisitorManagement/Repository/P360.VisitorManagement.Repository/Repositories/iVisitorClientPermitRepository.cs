using ClientPermitEntity = P360.VisitorManagement.Domain.Entities.VisitorClientPermit;
using P360.Repository.Repositories;

namespace P360.VisitorManagement.Repository.Repositories;

public interface IVisitorClientPermitRepository
    : IMongoRepository<ClientPermitEntity>
{
}
