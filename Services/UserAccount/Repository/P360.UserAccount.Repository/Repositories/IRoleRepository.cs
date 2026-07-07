using P360.Repository.Repositories;
using RoleEntity = P360.UserAccount.Domain.Entities.Role;

namespace P360.UserAccount.Repository.Repositories;

public interface IRoleRepository : IMongoRepository<RoleEntity>
{
    Task<RoleEntity?> GetByRoleIdAsync(string roleId, CancellationToken cancellationToken = default);

    Task<bool> RoleExistsAsync(string roleIdentifier, CancellationToken cancellationToken = default);
}
