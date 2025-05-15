using Microsoft.Extensions.DependencyInjection;

namespace CroosCutting.MS_AuthenticationAutorization.IoC
{
    public static class InjectDependencyExtensionsRepository
    {
        public static IServiceCollection InjectRepository(this IServiceCollection services)
        {
            return services;
        }
    }
}
