using UserEntity = P360.UserAccount.Domain.Entities.User;

namespace P360.UserAccount.Api.Contracts;

public sealed record CreateUserRequest(
    string? UserName,
    string? ShortName,
    string? ContactNo,
    string? Email,
    string? LoginPassword,
    string? ActiveDirectoryUserName,
    string? UserRoleId,
    string? CreatedBy,
    string? ClientId);

public sealed record UpdateUserRequest(
    string? UserName,
    string? ShortName,
    string? ContactNo,
    string? Email,
    string? LoginPassword,
    string? ActiveDirectoryUserName,
    string? UserRoleId);

public sealed record UserResponse(
    string Id,
    string UserId,
    string UserName,
    string ShortName,
    string ContactNo,
    string Email,
    string ActiveDirectoryUserName,
    string UserRoleId,
    string CreatedBy,
    DateTime CreatedDate,
    string ClientId,
    DateTime? LastLogin,
    string LoginStatus)
{
    public static UserResponse FromEntity(UserEntity user)
    {
        return new UserResponse(
            user.Id,
            user.UserId,
            user.UserName,
            user.ShortName,
            user.ContactNo,
            user.Email,
            user.ActiveDirectoryUserName,
            user.UserRoleId,
            user.CreatedBy,
            user.CreatedDate,
            user.ClientId,
            user.LastLogin,
            user.LoginStatus);
    }
}
