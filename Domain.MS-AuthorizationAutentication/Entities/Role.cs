using Domain.MS_AuthorizationAutentication.Model;

namespace Domain.MS_AuthorizationAutentication.Entities;

public class Role : BaseModel
{
    public string? Nome { get; set; }
    public string? Descricao { get; set; }

    public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
}
