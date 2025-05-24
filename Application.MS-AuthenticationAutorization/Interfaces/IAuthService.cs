using Application.MS_AuthenticationAutorization.Requests;
using Application.MS_AuthenticationAutorization.Responses;

namespace Application.MS_AuthenticationAutorization.Interfaces;

public interface IAuthService
{
    Task<AuthResponse> LoginAsync(LoginRequest loginRequest, CancellationToken cancellationToken);
}
