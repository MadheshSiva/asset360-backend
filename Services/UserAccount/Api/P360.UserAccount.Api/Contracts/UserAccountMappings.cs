using P360.UserAccount.Api.Security;
using AssignedPermissionEntity = P360.UserAccount.Domain.Entities.AssignedPermission;
//using AssignedProjectEntity = P360.UserAccount.Domain.Entities.AssignedProject;
//using AreaDetailEntity = P360.UserAccount.Domain.Entities.AreaDetail;
//using BuildingDetailEntity = P360.UserAccount.Domain.Entities.BuildingDetail;
//using CountryDetailEntity = P360.UserAccount.Domain.Entities.CountryDetail;
//using FloorDetailEntity = P360.UserAccount.Domain.Entities.FloorDetail;
using RoleEntity = P360.UserAccount.Domain.Entities.Role;
using UserEntity = P360.UserAccount.Domain.Entities.User;
//using ZoneDetailEntity = P360.UserAccount.Domain.Entities.ZoneDetail;

namespace P360.UserAccount.Api.Contracts;

internal static class UserAccountMappings
{
    public static UserEntity ToEntity(this CreateUserRequest request, PasswordHashingService passwordHashingService)
    {
        return new UserEntity
        {
            UserId = GeneratedIdentifier.Create("U"),
            UserName = Clean(request.UserName),
            ShortName = Clean(request.ShortName),
            ContactNo = Clean(request.ContactNo),
            Email = Clean(request.Email),
            LoginPassword = passwordHashingService.Hash(Clean(request.LoginPassword)),
            ActiveDirectoryUserName = Clean(request.ActiveDirectoryUserName),
            UserRoleId = Clean(request.UserRoleId),
            CreatedBy = Clean(request.CreatedBy),
            CreatedDate = DateTime.UtcNow,
            ClientId = Clean(request.ClientId),
            LoginStatus = "Active"
        };
    }

    public static void ApplyTo(this UpdateUserRequest request, UserEntity user, PasswordHashingService passwordHashingService)
    {
        user.UserName = Clean(request.UserName);
        user.ShortName = Clean(request.ShortName);
        user.ContactNo = Clean(request.ContactNo);
        user.Email = Clean(request.Email);
        user.LoginPassword = passwordHashingService.Hash(Clean(request.LoginPassword));
        user.ActiveDirectoryUserName = Clean(request.ActiveDirectoryUserName);
        user.UserRoleId = Clean(request.UserRoleId);
    }

    public static RoleEntity ToEntity(this CreateRoleRequest request)
    {
        return new RoleEntity
        {
            RoleId = GeneratedIdentifier.Create("R"),
            RoleName = Clean(request.RoleName),
            Description = Clean(request.Description),
            //AssignedProjects = ToAssignedProjects(request.AssignedProjects),
            AssignedPermissions = ToAssignedPermissions(request.AssignedPermissions),
            CreatedBy = Clean(request.CreatedBy),
            CreatedDate = DateTime.UtcNow,
            ClientId = Clean(request.ClientId),
            Action = "Created"
        };
    }

    public static void ApplyTo(this UpdateRoleRequest request, RoleEntity role)
    {
        role.RoleName = Clean(request.RoleName);
        role.Description = Clean(request.Description);
        //role.AssignedProjects = ToAssignedProjects(request.AssignedProjects);
        role.AssignedPermissions = ToAssignedPermissions(request.AssignedPermissions);
        role.Action = "Updated";
    }

    // private static List<AssignedProjectEntity> ToAssignedProjects(
    //     IReadOnlyCollection<AssignedProjectRequest>? assignedProjects)
    // {
    //     return assignedProjects?
    //         .Select(project => new AssignedProjectEntity
    //         {
    //             ProjectId = Clean(project.ProjectId),
    //             ProjectName = Clean(project.ProjectName),
    //             CountryDetails = project.CountryDetails?
    //                 .Select(country => new CountryDetailEntity
    //                 {
    //                     CountryId = Clean(country.CountryId),
    //                     CountryName = Clean(country.CountryName)
    //                 })
    //                 .ToList() ?? [],
    //             AreaDetails = project.AreaDetails?
    //                 .Select(area => new AreaDetailEntity
    //                 {
    //                     AreaId = Clean(area.AreaId),
    //                     AreaName = Clean(area.AreaName)
    //                 })
    //                 .ToList() ?? [],
    //             BuildingDetails = project.BuildingDetails?
    //                 .Select(building => new BuildingDetailEntity
    //                 {
    //                     BuildingId = Clean(building.BuildingId),
    //                     BuildingName = Clean(building.BuildingName)
    //                 })
    //                 .ToList() ?? [],
    //             FloorDetails = project.FloorDetails?
    //                 .Select(floor => new FloorDetailEntity
    //                 {
    //                     FloorId = Clean(floor.FloorId),
    //                     FloorName = Clean(floor.FloorName)
    //                 })
    //                 .ToList() ?? [],
    //             ZoneDetails = project.ZoneDetails?
    //                 .Select(zone => new ZoneDetailEntity
    //                 {
    //                     ZoneId = Clean(zone.ZoneId),
    //                     ZoneName = Clean(zone.ZoneName)
    //                 })
    //                 .ToList() ?? []
    //         })
    //         .ToList() ?? [];
    // }

    private static List<AssignedPermissionEntity> ToAssignedPermissions(
        IReadOnlyCollection<AssignedPermissionRequest>? assignedPermissions)
    {
        return assignedPermissions?
            .Select(permission => new AssignedPermissionEntity
            {
                FeatureName = Clean(permission.FeatureName),
                ViewOption = permission.ViewOption,
                EditOption = permission.EditOption
            })
            .ToList() ?? [];
    }

    private static string Clean(string? value)
    {
        return value?.Trim() ?? string.Empty;
    }
}
