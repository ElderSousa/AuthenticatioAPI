using Domain.MS_AuthorizationAutentication.Entities;
using Infrastructure.MS_AuthenticationAutorization.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.MS_AuthenticationAutorization.Repository;

public class RefreshTokenRepository : BaseRepository<RefreshToken>, IRefreshTokenRepository
{
    public RefreshTokenRepository(AuthDbContext authDbContext) : base(authDbContext) {}
    public async Task<bool> CreateAsync(RefreshToken refreshToken, CancellationToken cancellationToken)
    {
        return await GenericAddAsync(refreshToken, cancellationToken);
    }

    public async Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken)
    {
        return await authDbContext.refreshTokens
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Token == token && t.DeletedOn == null);
    }

    public async Task<bool> RevokeAsync(RefreshToken refreshToken, CancellationToken cancellationToken)
    {
        return await GenericUpdateAsync(refreshToken, cancellationToken);
    }
}
