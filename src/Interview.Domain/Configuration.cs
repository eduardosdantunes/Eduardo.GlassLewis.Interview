using Microsoft.Extensions.DependencyInjection;

namespace Interview.Domain;

public static class Configuration
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        return services;
    }
}