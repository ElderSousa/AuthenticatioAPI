using System.ComponentModel.DataAnnotations.Schema;

namespace Application.MS_AuthenticationAutorization.Requests;

public class RoleRequest
{
    public class CreateRoleRequest 
    {
        public string? Name { get; set; }
        public string? Description { get; set; }

    }

    public class UpdateRoleRequest
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

    }
}
