namespace Application.MS_AuthenticationAutorization.Requests;

public class UserRoleRequest
{
    public class CreateUserRoleRequest
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }

    public class UpdateUserRoleRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

    }
}
