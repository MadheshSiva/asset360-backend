using MongoDB.Driver;
using P360.Repository.Repositories;
using UserEntity = P360.UserAccount.Domain.Entities.User;

namespace P360.UserAccount.Repository.Repositories;

public sealed class UserRepository : MongoRepository<UserEntity>, IUserRepository, IMongoIndexConfigurator
{
    public const string CollectionName = "Users";
    private static readonly Collation CaseInsensitiveCollation = new("en", strength: CollationStrength.Secondary);

    public UserRepository(IMongoDatabase database)
        : base(database.GetCollection<UserEntity>(CollectionName))
    {
    }

    public async Task<bool> EmailExistsAsync(
        string email,
        string? excludedId = null,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return false;
        }

        var emailFilter = Builders<UserEntity>.Filter.Eq(user => user.Email, email.Trim());

        var filter = MongoObjectId.IsValid(excludedId)
            ? Builders<UserEntity>.Filter.And(
                emailFilter,
                Builders<UserEntity>.Filter.Ne(user => user.Id, excludedId))
            : emailFilter;

        return await Collection
            .Find(filter, new FindOptions { Collation = CaseInsensitiveCollation })
            .AnyAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<UserEntity>> GetByRoleIdAsync(
        string roleId,
        CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(user => user.UserRoleId == roleId)
            .ToListAsync(cancellationToken);
    }

    public async Task CreateIndexesAsync(CancellationToken cancellationToken = default)
    {
        var indexes = new[]
        {
            new CreateIndexModel<UserEntity>(
                Builders<UserEntity>.IndexKeys.Ascending(user => user.UserId),
                new CreateIndexOptions { Name = "ux_users_user_id", Unique = true }),
            new CreateIndexModel<UserEntity>(
                Builders<UserEntity>.IndexKeys.Ascending(user => user.Email),
                new CreateIndexOptions
                {
                    Name = "ux_users_email",
                    Unique = true,
                    Collation = CaseInsensitiveCollation
                }),
            new CreateIndexModel<UserEntity>(
                Builders<UserEntity>.IndexKeys.Ascending(user => user.UserRoleId),
                new CreateIndexOptions { Name = "ix_users_user_role_id" }),
            new CreateIndexModel<UserEntity>(
                Builders<UserEntity>.IndexKeys
                    .Ascending(user => user.ClientId)
                    .Ascending(user => user.LoginStatus),
                new CreateIndexOptions { Name = "ix_users_client_login_status" })
        };

        await Collection.Indexes.CreateManyAsync(indexes, cancellationToken);
    }
}
