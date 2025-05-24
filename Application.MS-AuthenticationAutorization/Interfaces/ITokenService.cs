using Domain.MS_AuthorizationAutentication.Entities;

namespace Application.MS_AuthenticationAutorization.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user, List<Role> roles);
}
