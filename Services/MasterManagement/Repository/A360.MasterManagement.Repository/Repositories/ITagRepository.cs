using A360.Repository.Repositories;
using TagEntity = A360.MasterManagement.Domain.Entities.Tag;

namespace A360.MasterManagement.Repository.Repositories;

public interface ITagRepository : IMongoRepository<TagEntity>
{
}
