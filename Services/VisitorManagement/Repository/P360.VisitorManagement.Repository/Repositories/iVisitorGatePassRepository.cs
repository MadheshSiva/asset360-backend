using GatePassEntity = P360.VisitorManagement.Domain.Entities.VisitorGatePass;
using P360.Repository.Repositories;

namespace P360.VisitorManagement.Repository.Repositories;

public interface IVisitorGatePassRepository
    : IMongoRepository<GatePassEntity>
{
}
