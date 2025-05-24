using Application.MS_AuthenticationAutorization.Responses;
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
            Active = userRequest.Active,
        };
    }

    public static User MapToUser(this UpdateUserRequest userRequest, UserResponse userResponse)
    {
        return new User
        {
            Id = userRequest.Id,
            Email = userRequest.Email,
            PasswordHash = userRequest.PasswordHash,
            Active = userRequest.Active,
            CreatedOn = userResponse.CreatedOn,
            CreatedBy = userResponse.CreatedBy
        };
    }

    public static User MapToUser(this UserResponse userResponse)
    {
        return new User
        {
            Id = userResponse.Id,
            Email = userResponse.Email,
            PasswordHash = userResponse.PasswordHash,
            Active = userResponse.Active,
            CreatedOn = userResponse.CreatedOn,
            CreatedBy = userResponse.CreatedBy,
            ModifiedOn = userResponse.ModifiedOn,
            ModifiedBy = userResponse.ModifiedBy
        };
    }

    public static UserResponse MapToUserResponse(this User user)
    {
        return new UserResponse
        {
            Id = user.Id,
            Email = user.Email,
            PasswordHash = user.PasswordHash,
            Active = user.Active,
            CreatedOn = user.CreatedOn,
            CreatedBy = user.CreatedBy,
            ModifiedOn = user.ModifiedOn,
            ModifiedBy = user.ModifiedBy
        };
    }

    public static IEnumerable<UserResponse> MapToUserResponse(this IEnumerable<User> users)
    {
        return users.Select(u => u.MapToUserResponse());
    }
}
