using ApprovalEntity = A360.VisitorManagement.Domain.Entities.VisitorApproval;
using A360.Repository.Repositories;

namespace A360.VisitorManagement.Repository.Repositories;

public interface IVisitorApprovalRepository
    : IMongoRepository<ApprovalEntity>
{
    Task<ApprovalEntity?> GetByPermitTypeAsync(
        string permitType,
        CancellationToken cancellationToken = default);
}
