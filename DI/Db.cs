using JwtAuthSample.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JwtAuthSample.DI;

public static class Db
{
    public static IServiceCollection AddOracleDb(this IServiceCollection services, IConfiguration config)
    {
        services.AddSingleton<IDbConnectionFactory, OracleDbConnectionFactory>();
        return services;
    }
}
