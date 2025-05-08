using Domain.MS_AuthorizationAutentication.Entities;

namespace Domain.MS_AuthorizationAutentication.Interfaces;

public interface IUserRoleRepository
{
    Task<bool> CreateAsync(UserRole role);
    Task<UserRole> GetIdAsync(Guid id);
    Task<IEnumerable<UserRole>> GetAllAsync();
    Task<bool> UpdateAsync(UserRole role);
    Task<bool> DeleteAsync(Guid id);
}
