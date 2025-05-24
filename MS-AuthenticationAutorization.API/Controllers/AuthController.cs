using Application.MS_AuthenticationAutorization.Interfaces;
using Application.MS_AuthenticationAutorization.Requests;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace MS_AuthenticationAutorization.API.Controllers;
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiversion}/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Realiza login e gera um token JWT com base nas credenciais informadas.
    /// </summary>
    /// <param name="loginRequest">Credenciais de login (email e senha).</param>
    /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
    /// <returns>Token JWT gerado se o login for bem-sucedido.</returns>
    [HttpPost]
    public async Task<IActionResult> LoginAsync(LoginRequest loginRequest, CancellationToken cancellationToken)
    {
        var token = await _authService.LoginAsync(loginRequest, cancellationToken);
        return Ok(token);
    }
}
