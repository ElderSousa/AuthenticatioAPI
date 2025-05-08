using Domain.MS_AuthorizationAutentication.Entities;
using Domain.MS_AuthorizationAutentication.Interfaces;
using Infrastructure.MS_AuthenticationAutorization.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.MS_AuthenticationAutorization.Repository;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(AuthDbContext authDbContext): base(authDbContext) { }

    public async Task<bool> CreateAsync(User user, CancellationToken cancellationToken)
    {
        return await GenericAddAsync(user, cancellationToken);
    }

    public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await authDbContext.Users
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<User?> GetIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await authDbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<bool> UpdateAsync(User user, CancellationToken cancellationToken)
    {
        return await GenericUpdateAsync(user, cancellationToken);
    }
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = GetIdAsync(id, cancellationToken).Result;
        if (user is null)
            return false;

        return await GenericUpdateAsync(user, cancellationToken);
    }
}