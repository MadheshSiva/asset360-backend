using PanelSettingEntity = A360.VisitorManagement.Domain.Entities.VisitorPanelSetting;
using A360.Repository.Repositories;

namespace A360.VisitorManagement.Repository.Repositories;

public interface IVisitorManagementRepository
    : IMongoRepository<PanelSettingEntity>
{
    Task<PanelSettingEntity?> GetByClientIdAsync(
        string clientId,
        CancellationToken cancellationToken = default);
}
