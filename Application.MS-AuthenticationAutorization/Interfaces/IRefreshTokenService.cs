using Domain.MS_AuthorizationAutentication.Entities;

namespace Application.MS_AuthenticationAutorization.Interfaces
{
    public interface IRefreshTokenService
    {
        Task<string> CreateAsync(User user, CancellationToken cancellationToken);
        Task<RefreshToken> GetByTokenAsync(string token, CancellationToken cancellationToken);
        Task<bool> RevokeAsync(RefreshToken refreshToken, CancellationToken cancellationToken);
    }
}
