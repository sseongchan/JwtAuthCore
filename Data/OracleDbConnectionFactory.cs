using System.Data;

namespace JwtAuthSample.Data
{
    public class OracleDbConnectionFactory(IConfiguration configuration) : IDbConnectionFactory
    {
        private readonly string _connectionString = configuration.GetConnectionString("OracleDb")!;

        public IDbConnection CreateConnection()
        {
            return new Oracle.ManagedDataAccess.Client.OracleConnection(_connectionString);
        }
    }
}
