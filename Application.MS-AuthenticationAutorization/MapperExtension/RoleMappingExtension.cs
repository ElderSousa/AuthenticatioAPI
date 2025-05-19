using Domain.MS_AuthorizationAutentication.Entities;
using static Application.MS_AuthenticationAutorization.Requests.RoleRequest;

namespace Application.MS_AuthenticationAutorization.MapperExtension;

public static class RoleMappingExtension
{
    public static Role RoleToMap(this CreateRoleRequest createRoleRequest)
    {
        return new Role
        {
            Name = createRoleRequest.Name,
            Description = createRoleRequest.Description,
        };
    }

    public static Role RoleToMap(this UpdateRoleRequest updateCreateRoleRequest)
    {
        return new Role
        {
            Id = updateCreateRoleRequest.Id,
            Name = updateCreateRoleRequest.Name,
            Description = updateCreateRoleRequest.Description,
        };
    }

    public static  RoleToMap(this UpdateRoleRequest updateCreateRoleRequest)
    {
        return new Role
        {
            Id = updateCreateRoleRequest.Id,
            Name = updateCreateRoleRequest.Name,
            Description = updateCreateRoleRequest.Description,
        };
    }
}
