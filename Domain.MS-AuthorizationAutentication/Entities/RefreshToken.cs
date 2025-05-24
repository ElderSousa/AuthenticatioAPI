using Domain.MS_AuthorizationAutentication.Model;

namespace Domain.MS_AuthorizationAutentication.Entities;

public class RefreshToken : BaseModel
{
    public string Token { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public DateTime Expiration { get; set; }
    public bool IsRevoked { get; set; } = false;

    public User User { get; set; } = new();
}
