using Application.MS_AuthenticationAutorization.Requests;
using Application.MS_AuthenticationAutorization.Responses;
using Domain.MS_AuthorizationAutentication.Entities;

namespace Application.MS_AuthenticationAutorization.MapperExtension;

public static class UserExtension
{
    public static User MapToUser(this UserRequest.CreateUserRequest userRequest)
    {
        return new User 
        {
            Email = userRequest.Email,
            PasswordHash = userRequest.PasswordHash,
            Ativo = userRequest.Ativo,
            typeUserRole = userRequest.typeUserRole
        };
    }

    public static User MapToUser(this UserRequest.UpdateUserRequest userRequest)
    {
        return new User
        {
            Id = userRequest.Id,
            Email = userRequest.Email,
            PasswordHash = userRequest.PasswordHash,
            Ativo = userRequest.Ativo,
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
            Ativo = user.Ativo,
            typeUserRole = user.typeUserRole,
            CriadoEm = user.CriadoEm,
            CriadoPor = user.CriadoPor,
            ModificadoEm = user.ModificadoEm,
            ModificadoPor = user.ModificadoPor
        };
    }
}
