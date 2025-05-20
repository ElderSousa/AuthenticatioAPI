using Application.MS_AuthenticationAutorization.Responses;
using Domain.MS_AuthorizationAutentication.Entities;
using System.Data;
using static Application.MS_AuthenticationAutorization.Requests.UserRoleRequest;

namespace Application.MS_AuthenticationAutorization.MapperExtension;

public static class UserRoleMappingExtension
{
    public static UserRole MapToUseRole(this CreateUserRoleRequest createUserRoleRequest)
    {
        return new UserRole
        {
            UserId = createUserRoleRequest.UserId,
            RoleId = createUserRoleRequest.RoleId,
        };
    }

    public static UserRole MapToUseRole(this UpdateUserRoleRequest updateUserRoleRequest)
    {
        return new UserRole
        {
            UserId = updateUserRoleRequest.UserId,
            RoleId = updateUserRoleRequest.RoleId,
        };
    }

    public static UserRoleResponse MapToUseRoleResponse(this UserRole userRole)
    {
        return new UserRoleResponse
        {
            UserId = userRole.UserId,
            RoleId = userRole.RoleId,
            CreatedOn = userRole.CreatedOn,
            CreatedBy = userRole.CreatedBy,
            ModifiedOn = userRole.ModifiedOn,
            ModifiedBy = userRole.ModifiedBy
        };
    }

    public static IEnumerable<UserRoleResponse> MapToUseRoleResponse(this IEnumerable<UserRole> userRoles)
    {
        return userRoles.Select(ur => ur.MapToUseRoleResponse());
    }
}
