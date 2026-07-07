using EntryExitEntity = P360.VisitorManagement.Domain.Entities.VisitorEntryExit;
using P360.Repository.Repositories;

namespace P360.VisitorManagement.Repository.Repositories;

public interface IVisitorEntryExitRepository
    : IMongoRepository<EntryExitEntity>
{
}
