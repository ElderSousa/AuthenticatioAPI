using Domain.MS_AuthorizationAutentication.Entities;

namespace Domain.MS_AuthorizationAutentication.Interfaces;

public interface IRoleRepositoy
{
    Task<bool> CreateAsync(Role Role);
    Task<Role> GetIdAsync(Guid id);
    Task<Role> GetByNomeAsync(string nome);
    Task<IEnumerable<Role>> GetAllAsync();
    Task<bool> UpdateAsync(Role role);
    Task<bool> DeleteAsync(Guid id);
}
