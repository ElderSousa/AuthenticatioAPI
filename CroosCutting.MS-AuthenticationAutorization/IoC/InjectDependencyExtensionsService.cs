using Application.MS_AuthenticationAutorization.Interfaces;
using Application.MS_AuthenticationAutorization.Services;
using Application.MS_AuthenticationAutorization.Validation;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CroosCutting.MS_AuthenticationAutorization.IoC;

public static class InjectDependencyExtensionsService
{
    public static IServiceCollection InjectService(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddValidatorsFromAssemblyContaining<UserValidation>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IUserRoleService, UserRoleService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}
