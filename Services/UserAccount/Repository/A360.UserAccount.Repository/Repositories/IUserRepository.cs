using A360.Repository.Repositories;
using UserEntity = A360.UserAccount.Domain.Entities.User;

namespace A360.UserAccount.Repository.Repositories;

public interface IUserRepository : IMongoRepository<UserEntity>
{
    Task<bool> EmailExistsAsync(string email, string? excludedId = null, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<UserEntity>> GetByRoleIdAsync(string roleId, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<UserEntity>> GetByUserNameAsync(string userName, int limit, CancellationToken cancellationToken = default);

    Task SetTwoFactorCodeAsync(string id, string codeHash, DateTime expiration, CancellationToken cancellationToken = default);

    Task<int> RegisterFailedTwoFactorAttemptAsync(string id, CancellationToken cancellationToken = default);

    Task ClearTwoFactorCodeAsync(string id, CancellationToken cancellationToken = default);

    Task<bool> CompleteTwoFactorLoginAsync(string id, string codeHash, CancellationToken cancellationToken = default);
}
