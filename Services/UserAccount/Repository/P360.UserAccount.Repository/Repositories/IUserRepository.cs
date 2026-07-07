using P360.Repository.Repositories;
using UserEntity = P360.UserAccount.Domain.Entities.User;

namespace P360.UserAccount.Repository.Repositories;

public interface IUserRepository : IMongoRepository<UserEntity>
{
    Task<bool> EmailExistsAsync(string email, string? excludedId = null, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<UserEntity>> GetByRoleIdAsync(string roleId, CancellationToken cancellationToken = default);
}
