using ClientPermitEntity = A360.VisitorManagement.Domain.Entities.VisitorClientPermit;
using A360.Repository.Repositories;

namespace A360.VisitorManagement.Repository.Repositories;

public interface IVisitorClientPermitRepository
    : IMongoRepository<ClientPermitEntity>
{
}
