using Application.MS_AuthenticationAutorization.Requests;
using Application.MS_AuthenticationAutorization.Responses;
using Application.MS_AuthenticationAutorization.Services;
using Domain.MS_AuthorizationAutentication.Entities;
using static Application.MS_AuthenticationAutorization.Requests.UserRequest;

namespace Application.MS_AuthenticationAutorization.MapperExtension;

public static class UserMappingExtension
{
    public static User MapToUser(this CreateUserRequest userRequest)
    {
        return new User 
        {
            Email = userRequest.Email,
            PasswordHash = userRequest.PasswordHash,
            Active = userRequest.Ativo,
            typeUserRole = userRequest.typeUserRole
        };
    }

    public static User MapToUser(this UpdateUserRequest userRequest)
    {
        return new User
        {
            Id = userRequest.Id,
            Email = userRequest.Email,
            PasswordHash = userRequest.PasswordHash,
            Active = userRequest.Ativo,
            typeUserRole = userRequest.typeUserRole
        };
    }

    public static UserResponse MapToUserResponse(this User user)
    {
        return new UserResponse
        {
            Id = user.Id,
            Email = user.Email,
            PasswordHash = user.PasswordHash,
            Ativo = user.Active,
            typeUserRole = user.typeUserRole,
            CriadoEm = user.CreatedOn,
            CriadoPor = user.CreatedBy,
            ModificadoEm = user.ModifiedOn,
            ModificadoPor = user.ModifiedBy
        };
    }

    public static IEnumerable<UserResponse> MapToUserResponse(this IEnumerable<User> users)
    {
        return users.Select(u => u.MapToUserResponse());
    }
}
