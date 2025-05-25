using Microsoft.Extensions.DependencyInjection;

namespace JwtAuthSample.DI;

public static class Policy
{
    public static IServiceCollection AddAppPolicy(this IServiceCollection services)
    {
        services.AddAuthorization();
        return services;
    }
}
