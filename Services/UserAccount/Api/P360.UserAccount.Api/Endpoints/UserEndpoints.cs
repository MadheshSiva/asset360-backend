using P360.Repository.Repositories;
using P360.UserAccount.Api.Contracts;
using P360.UserAccount.Api.Security;
using P360.UserAccount.Api.Validation;
using P360.UserAccount.Repository.Repositories;

namespace P360.UserAccount.Api.Endpoints;

public static class UserEndpoints
{
    public static RouteGroupBuilder MapUserEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/users").WithTags("Users");

        group.MapGet("", GetUsersAsync).WithName("GetUsers");
        group.MapGet("/by-role/{roleId}", GetUsersByRoleAsync).WithName("GetUsersByRole");
        group.MapGet("/{id}", GetUserByIdAsync).WithName("GetUserById");
        group.MapPost("", CreateUserAsync).WithName("CreateUser");
        group.MapPut("/{id}", UpdateUserAsync).WithName("UpdateUser");
        group.MapDelete("/{id}", DeleteUserAsync).WithName("DeleteUser");

        return group;
    }

    private static async Task<IResult> GetUsersAsync(
        IUserRepository repository,
        CancellationToken cancellationToken)
    {
        var users = await repository.GetAllAsync(cancellationToken);
        return Results.Ok(users.Select(UserResponse.FromEntity));
    }

    private static async Task<IResult> GetUsersByRoleAsync(
        string roleId,
        IUserRepository repository,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(roleId))
        {
            return Results.BadRequest(new { message = "Role id is required." });
        }

        var users = await repository.GetByRoleIdAsync(roleId.Trim(), cancellationToken);
        return Results.Ok(users.Select(UserResponse.FromEntity));
    }

    private static async Task<IResult> GetUserByIdAsync(
        string id,
        IUserRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid user id." });
        }

        var user = await repository.GetByIdAsync(id, cancellationToken);
        return user is null ? Results.NotFound() : Results.Ok(UserResponse.FromEntity(user));
    }

    private static async Task<IResult> CreateUserAsync(
        CreateUserRequest request,
        IUserRepository repository,
        IRoleRepository roleRepository,
        PasswordHashingService passwordHashingService,
        CancellationToken cancellationToken)
    {
        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        if (await repository.EmailExistsAsync(request.Email!, cancellationToken: cancellationToken))
        {
            return Results.Conflict(new { message = "Email already exists." });
        }

        var relationshipErrors = await request.ValidateRelationshipsAsync(roleRepository, cancellationToken);
        if (relationshipErrors.Count > 0)
        {
            return Results.ValidationProblem(relationshipErrors);
        }

        var user = await repository.CreateAsync(request.ToEntity(passwordHashingService), cancellationToken);
        return Results.Created($"/api/users/{user.Id}", UserResponse.FromEntity(user));
    }

    private static async Task<IResult> UpdateUserAsync(
        string id,
        UpdateUserRequest request,
        IUserRepository repository,
        IRoleRepository roleRepository,
        PasswordHashingService passwordHashingService,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid user id." });
        }

        var validationErrors = request.Validate();
        if (validationErrors.Count > 0)
        {
            return Results.ValidationProblem(validationErrors);
        }

        if (await repository.EmailExistsAsync(request.Email!, id, cancellationToken))
        {
            return Results.Conflict(new { message = "Email already exists." });
        }

        var relationshipErrors = await request.ValidateRelationshipsAsync(roleRepository, cancellationToken);
        if (relationshipErrors.Count > 0)
        {
            return Results.ValidationProblem(relationshipErrors);
        }

        var user = await repository.GetByIdAsync(id, cancellationToken);
        if (user is null)
        {
            return Results.NotFound();
        }

        request.ApplyTo(user, passwordHashingService);

        var updated = await repository.UpdateAsync(id, user, cancellationToken);
        return updated ? Results.Ok(UserResponse.FromEntity(user)) : Results.NotFound();
    }

    private static async Task<IResult> DeleteUserAsync(
        string id,
        IUserRepository repository,
        CancellationToken cancellationToken)
    {
        if (!MongoObjectId.IsValid(id))
        {
            return Results.BadRequest(new { message = "Invalid user id." });
        }

        var deleted = await repository.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
