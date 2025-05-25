using JwtAuthSample.Modules.User.Repositories;
using JwtAuthSample.Modules.User.Services;
using Scrutor;

namespace JwtAuthSample.DI;

public static class Register
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.Scan(scan => scan
            .FromApplicationDependencies()
            .AddClasses(c => c.Where(t =>
                t.Name.EndsWith("Service") || t.Name.EndsWith("Repository")))
            .AsMatchingInterface()
            .WithScopedLifetime());

        return services;
    }
}
