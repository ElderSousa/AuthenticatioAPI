using Domain.MS_AuthorizationAutentication.Interfaces;
using Infrastructure.MS_AuthenticationAutorization.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace CroosCutting.MS_AuthenticationAutorization.IoC
{
    public static class InjectDependencyExtensionsRepository
    {
        public static IServiceCollection InjectRepository(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepositoy, RoleRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            return services;
        }
    }
}
