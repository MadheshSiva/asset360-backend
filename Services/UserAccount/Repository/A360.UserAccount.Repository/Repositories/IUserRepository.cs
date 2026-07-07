using A360.Repository.Repositories;
using UserEntity = A360.UserAccount.Domain.Entities.User;

namespace A360.UserAccount.Repository.Repositories;

public interface IUserRepository : IMongoRepository<UserEntity>
{
    Task<bool> EmailExistsAsync(string email, string? excludedId = null, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<UserEntity>> GetByRoleIdAsync(string roleId, CancellationToken cancellationToken = default);
}
