using A360.Repository.Repositories;
using SkillMasterEntity = A360.MasterManagement.Domain.Entities.SkillMaster;

namespace A360.MasterManagement.Repository.Repositories;

public interface ISkillMasterRepository : IMongoRepository<SkillMasterEntity>
{
}
