using Domain.MS_AuthorizationAutentication.Entities;

namespace Domain.MS_AuthorizationAutentication.Interfaces;

public interface IUserRoleRepository
{
    Task<bool> CreateAsync(UserRole UserRole, CancellationToken cancellationToken);
    Task<UserRole> GetIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<UserRole>> GetAllAsync(CancellationToken cancellationToken);
    Task<bool> UpdateAsync(UserRole userRole, CancellationToken cancellationToken);
    Task<bool> SoftDeleteAsync(UserRole userRole, CancellationToken cancellationToken);
    Task<bool> UserExistAsync(Guid id, CancellationToken cancellationToken);
}
