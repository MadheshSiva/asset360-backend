using A360.Repository.Repositories;
using CategoryEntity = A360.MasterManagement.Domain.Entities.Category;

namespace A360.MasterManagement.Repository.Repositories;

public interface ICategoryRepository : IMongoRepository<CategoryEntity>
{
}
