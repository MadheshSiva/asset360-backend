using A360.Repository.Repositories;
using IssueTypeMasterEntity = A360.MasterManagement.Domain.Entities.IssueTypeMaster;

namespace A360.MasterManagement.Repository.Repositories;

public interface IIssueTypeMasterRepository : IMongoRepository<IssueTypeMasterEntity>
{
}
