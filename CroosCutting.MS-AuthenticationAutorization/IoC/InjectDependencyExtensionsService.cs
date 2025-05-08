using Microsoft.Extensions.DependencyInjection;

namespace CroosCutting.MS_AuthenticationAutorization.IoC;

public static class InjectDependencyExtensionsService
{
    public static IServiceCollection InjectService(this IServiceCollection services)
    {
        return services;
    }
}
