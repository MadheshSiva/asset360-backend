using EntryExitEntity = A360.VisitorManagement.Domain.Entities.VisitorEntryExit;
using A360.Repository.Repositories;

namespace A360.VisitorManagement.Repository.Repositories;

public interface IVisitorEntryExitRepository
    : IMongoRepository<EntryExitEntity>
{
}
