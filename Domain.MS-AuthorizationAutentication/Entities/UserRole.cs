using System.ComponentModel.DataAnnotations.Schema;
using Domain.MS_AuthorizationAutentication.Model;

namespace Domain.MS_AuthorizationAutentication.Entities;

public class UserRole : BaseModel
{ 
    public Guid UserId { get; set; }
    public User User { get; set; } = new();

    public Guid RoleId { get; set; }
    public Role Role { get; set; } = new();

    [NotMapped]
    public bool ValidationRegister { get; set; }

}
