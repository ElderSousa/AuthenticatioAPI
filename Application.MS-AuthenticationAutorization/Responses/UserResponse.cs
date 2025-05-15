using Domain.MS_AuthorizationAutentication.Enums;

namespace Application.MS_AuthenticationAutorization.Responses;

public class UserResponse
{
    public Guid Id { get; set; }
    public string? Email { get; set; }
    public string? PasswordHash { get; set; }
    public bool Ativo { get; set; }
    public TypeUserRole typeUserRole { get; set; }
    public Guid CriadoPor { get; set; }
    public Guid ModificadoPor { get; set; }
    public DateTime CriadoEm { get; set; }
    public DateTime ModificadoEm { get; set; }
}
