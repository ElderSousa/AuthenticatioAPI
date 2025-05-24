using Application.MS_AuthenticationAutorization.Interfaces;
using Application.MS_AuthenticationAutorization.MapperExtension;
using Application.MS_AuthenticationAutorization.Requests;
using Application.MS_AuthenticationAutorization.Responses;
using Domain.MS_AuthorizationAutentication.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Win32.SafeHandles;

namespace Application.MS_AuthenticationAutorization.Services;

public class AuthService : BaseService, IAuthService
{
    private readonly IUserService _userService;
    private readonly IUserRepository _userRepository;
    private readonly IValidator<LoginRequest> _loginValidator;
    private readonly ITokenService _tokenService;

    private Response _response = new();

    public AuthService(IUserService userService,
        IUserRepository userRepository,
        IValidator<LoginRequest> loginValidator,
        ITokenService tokenService,
        IHttpContextAccessor contextAccessor,
        ILogger<AuthService> logger) : base(contextAccessor, logger)
    {
        _userService = userService;
        _userRepository = userRepository;
        _loginValidator = loginValidator;
        _tokenService = tokenService;
    }
    public async Task<AuthResponse> LoginAsync(LoginRequest loginRequest, CancellationToken cancellationToken)
    {
        var user = UserMappingExtension.MapToUser(await _userService.GetByEmailAsync(loginRequest.Email, cancellationToken));

        _response = await ExecuteValidationResponseAsync(_loginValidator, loginRequest);
        if (_response.Error)
            throw new ArgumentException(_response.Status);

        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.PasswordHash);

        if (!isPasswordValid)
            throw new UnauthorizedAccessException("Credenciais inválidas.");

        var roles = await _userRepository.GetRolesAsync(user.Id, cancellationToken);
        if (!roles.Any())
            throw new KeyNotFoundException("Não foram encontradas roles em nossa base de dados.");

        //var (accessTolen, refreshToken) = _tokenService.GenerateToken(user, roles.ToList());

        //var refreshTokenEntity = new Re

        return null;
    }
}
