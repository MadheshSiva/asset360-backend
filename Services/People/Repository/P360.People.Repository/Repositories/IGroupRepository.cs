using GroupEntity = P360.People.Domain.Entities.Group;
using P360.Repository.Repositories;

namespace P360.People.Repository.Repositories;

public interface IGroupRepository : IMongoRepository<GroupEntity>
{
    
     Task<IEnumerable<GroupEntity>> GetByGroupTypeAsync(
        string groupType,
        CancellationToken cancellationToken = default);

       Task<GroupEntity?> GetByIdAsync(
        string id,
        CancellationToken cancellationToken = default);
}
