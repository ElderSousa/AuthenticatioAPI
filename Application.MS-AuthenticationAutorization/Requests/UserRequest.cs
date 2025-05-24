namespace Application.MS_AuthenticationAutorization.Requests;

public class UserRequest
{
    public class CreateUserRequest
    {
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public bool Active { get; set; }
    }

    public class UpdateUserRequest
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public bool Active { get; set; }
    }

}
