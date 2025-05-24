using Application.MS_AuthenticationAutorization.Responses;
using Domain.MS_AuthorizationAutentication.Entities;
using System.Data;
using static Application.MS_AuthenticationAutorization.Requests.RoleRequest;

namespace Application.MS_AuthenticationAutorization.MapperExtension;

public static class RoleMappingExtension
{
    public static Role MapToRole(this CreateRoleRequest createRoleRequest)
    {
        return new Role
        {
            Name = createRoleRequest.Name,
            Description = createRoleRequest.Description,
        };
    }

    public static Role MapToRole(this UpdateRoleRequest updateRoleRequest, RoleResponse roleResponse)
    {
        return new Role
        {
            Id = updateRoleRequest.Id,
            Name = updateRoleRequest.Name,
            Description = updateRoleRequest.Description,
            CreatedOn = roleResponse.CreatedOn,
            CreatedBy = roleResponse.CreatedBy
        };
    }

    public static RoleResponse MapToRoleResponse(this Role role)
    {
        return new RoleResponse
        {
            Id = role.Id,
            Name = role.Name,
            Description = role.Description,
            CreatedOn = role.CreatedOn,
            CreatedBy = role.CreatedBy,
            ModifiedOn = role.ModifiedOn,
            ModifiedBy = role.ModifiedBy
        };
    }

    public static IEnumerable<RoleResponse> MapToRoleResponse(this IEnumerable<Role> roles)
    {
        return roles.Select(r => r.MapToRoleResponse());
    }
}
