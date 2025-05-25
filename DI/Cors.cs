using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JwtAuthSample.DI;

public static class Cors
{
    public static IServiceCollection AddAppCors(this IServiceCollection services, IConfiguration config)
    {
        services.AddCors(opt =>
        {
            opt.AddDefaultPolicy(builder =>
            {
                builder.WithOrigins("http://localhost:5173")
                       .AllowAnyHeader()
                       .AllowAnyMethod();
            });
        });

        return services;
    }
}
