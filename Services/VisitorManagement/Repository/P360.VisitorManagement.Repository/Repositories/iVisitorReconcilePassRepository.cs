using ReconcilePassEntity = P360.VisitorManagement.Domain.Entities.VisitorReconcilePass;
using P360.Repository.Repositories;

namespace P360.VisitorManagement.Repository.Repositories;

public interface IVisitorReconcilePassRepository
    : IMongoRepository<ReconcilePassEntity>
{
}
