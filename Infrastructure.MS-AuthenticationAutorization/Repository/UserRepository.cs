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
            .Where(u => u.DeletedOn == null)
            .ToListAsync(cancellationToken);
    }

    public async Task<User?> GetIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await authDbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id && u.DeletedOn == null, cancellationToken);
    }

    public async Task<bool> UpdateAsync(User user, CancellationToken cancellationToken)
    {
        return await GenericUpdateAsync(user, cancellationToken);
    }
    public async Task<bool> SoftDeleteAsync(User user, CancellationToken cancellationToken)
    {
        return await GenericUpdateAsync(user, cancellationToken);
    }

    public async Task<bool> UserExistAsync(Guid id, CancellationToken cancellationToken)
    {
        return await ExistAsync(u => u.Id == id && u.DeletedOn == null, cancellationToken);
    }
}