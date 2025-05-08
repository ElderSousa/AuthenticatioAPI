using Domain.MS_AuthorizationAutentication.Enums;
using Domain.MS_AuthorizationAutentication.Model;

namespace Domain.MS_AuthorizationAutentication.Entities;

public class User : BaseModel
{
    public string? Email { get; set; }
    public string? PasswordHash { get; set; }
    public bool Ativo { get; set; }
    public TypeUserRole typeUserRole { get; set; }

    public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
}
