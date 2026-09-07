using A360.Repository.Repositories;
using SiteEntity = A360.MasterManagement.Domain.Entities.Site;

namespace A360.MasterManagement.Repository.Repositories;

public interface ISiteRepository : IMongoRepository<SiteEntity>
{
    Task<SiteEntity?> GetBySiteCodeAsync(string siteCode, CancellationToken cancellationToken = default);
}
