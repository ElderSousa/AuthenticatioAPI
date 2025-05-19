using Domain.MS_AuthorizationAutentication.Entities;

namespace Domain.MS_AuthorizationAutentication.Interfaces;

public interface IRoleRepositoy
{
    Task<bool> CreateAsync(Role Role, CancellationToken cancellationToken);
    Task<Role?> GetIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Role?> GetByNomeAsync(string nome, CancellationToken cancellationToken);
    Task<IEnumerable<Role>> GetAllAsync(CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Role role, CancellationToken cancellationToken);
    Task<bool> SoftDeleteAsync(Role role, CancellationToken cancellationToken);
    Task<bool> UserExistAsync(Guid id, CancellationToken cancellationToken);
}
