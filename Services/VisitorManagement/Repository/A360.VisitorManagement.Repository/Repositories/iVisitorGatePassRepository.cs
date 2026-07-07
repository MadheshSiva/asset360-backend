using GatePassEntity = A360.VisitorManagement.Domain.Entities.VisitorGatePass;
using A360.Repository.Repositories;

namespace A360.VisitorManagement.Repository.Repositories;

public interface IVisitorGatePassRepository
    : IMongoRepository<GatePassEntity>
{
}
