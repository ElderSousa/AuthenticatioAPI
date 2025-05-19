using Domain.MS_AuthorizationAutentication.Entities;
using Domain.MS_AuthorizationAutentication.Interfaces;
using Infrastructure.MS_AuthenticationAutorization.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.MS_AuthenticationAutorization.Repository;

public class UserRoleRepository : BaseRepository<UserRole>, IUserRoleRepository
{
    public UserRoleRepository(AuthDbContext authDbContext) : base(authDbContext) { }

    public async Task<bool> CreateAsync(UserRole userRole, CancellationToken cancellationToken)
    {
        return await GenericAddAsync(userRole, cancellationToken);
    }

    public async Task<IEnumerable<UserRole>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await authDbContext.UserRoles
            .AsNoTracking()
            .Where(ur => ur.DeletedOn == null)
            .ToListAsync(cancellationToken);
    }

    public async Task<UserRole> GetIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await authDbContext.UserRoles
            .AsNoTracking()
            .FirstAsync(ur => ur.Id == id && ur.DeletedOn == null, cancellationToken);
    }

    public async Task<bool> UpdateAsync(UserRole userRole, CancellationToken cancellationToken)
    {
        return await GenericUpdateAsync(userRole, cancellationToken);
    }

    public async Task<bool> SoftDeleteAsync(UserRole userRole, CancellationToken cancellationToken)
    {
        return await GenericUpdateAsync(userRole, cancellationToken);
    }

    public async Task<bool> UserExistAsync(Guid id, CancellationToken cancellationToken)
    {
        return await ExistAsync(u => u.Id == id && u.DeletedOn == null, cancellationToken);
    }
}
