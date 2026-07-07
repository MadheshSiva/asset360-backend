using ApprovalEntity = P360.VisitorManagement.Domain.Entities.VisitorApproval;
using P360.Repository.Repositories;

namespace P360.VisitorManagement.Repository.Repositories;

public interface IVisitorApprovalRepository
    : IMongoRepository<ApprovalEntity>
{
    Task<ApprovalEntity?> GetByPermitTypeAsync(
        string permitType,
        CancellationToken cancellationToken = default);
}
