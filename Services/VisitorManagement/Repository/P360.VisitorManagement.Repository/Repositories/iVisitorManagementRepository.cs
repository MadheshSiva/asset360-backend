using PanelSettingEntity = P360.VisitorManagement.Domain.Entities.VisitorPanelSetting;
using P360.Repository.Repositories;

namespace P360.VisitorManagement.Repository.Repositories;

public interface IVisitorManagementRepository
    : IMongoRepository<PanelSettingEntity>
{
    Task<PanelSettingEntity?> GetByClientIdAsync(
        string clientId,
        CancellationToken cancellationToken = default);
}
