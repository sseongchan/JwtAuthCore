using Dapper;

namespace JwtAuthSample.DI;

public static class DapperConfig
{
    public static void ConfigureDapper()
    {
        DefaultTypeMap.MatchNamesWithUnderscores = true;
    }
}
