using System.Data;

namespace JwtAuthSample.Data
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
