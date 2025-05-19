using Domain.MS_AuthorizationAutentication.Entities;

namespace Domain.MS_AuthorizationAutentication.Interfaces;

public interface IUserRepository
{
    Task<bool> CreateAsync(User user, CancellationToken cancellationToken);
    Task<User?> GetIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken);
    Task<bool> UpdateAsync(User user, CancellationToken cancellationToken);
    Task<bool> SoftDeleteAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> UserExistAsync(Guid id, CancellationToken cancellationToken); 
}
