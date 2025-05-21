using System.ComponentModel.DataAnnotations.Schema;
using Domain.MS_AuthorizationAutentication.Model;

namespace Domain.MS_AuthorizationAutentication.Entities;

public class Role : BaseModel
{
    public string? Name { get; set; }
    public string? Description { get; set; }

    public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
}
