using ReconcilePassEntity = A360.VisitorManagement.Domain.Entities.VisitorReconcilePass;
using A360.Repository.Repositories;

namespace A360.VisitorManagement.Repository.Repositories;

public interface IVisitorReconcilePassRepository
    : IMongoRepository<ReconcilePassEntity>
{
}
