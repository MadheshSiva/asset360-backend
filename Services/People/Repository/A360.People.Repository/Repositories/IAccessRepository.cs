using AccessEntity = A360.People.Domain.Entities.Access;
using A360.Repository.Repositories;

namespace A360.People.Repository.Repositories;

public interface IAccessRepository : IMongoRepository<AccessEntity>
{
    

    
}