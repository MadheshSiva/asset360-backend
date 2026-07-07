using AccessEntity = P360.People.Domain.Entities.Access;
using P360.Repository.Repositories;

namespace P360.People.Repository.Repositories;

public interface IAccessRepository : IMongoRepository<AccessEntity>
{
    

    
}