using Dapper;
using JwtAuthSample.Data;
using JwtAuthSample.Modules.User.Dtos;

namespace JwtAuthSample.Modules.User.Repositories
{
    public class UserRepository(IDbConnectionFactory dbConnectionFactory) : IUserRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            using var conn = _dbConnectionFactory.CreateConnection();
            return await conn.QueryAsync<UserDto>("SELECT * FROM HR.USERS");
        }

        public async Task<UserDto?> GetByIdAsync(int userId)
        {
            using var conn = _dbConnectionFactory.CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<UserDto>("SELECT * FROM HR.USERS WHERE USER_ID = :UserId", new { UserId = userId });
        }
        public async Task<UserDto?> GetByUsernameAsync(string username)
        {
            using var conn = _dbConnectionFactory.CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<UserDto>("SELECT * FROM HR.USERS WHERE USERNAME = :Username", new { Username = username });
        }
        public async Task<string?> GetPasswordHashAsync(string username)
        {
            using var conn = _dbConnectionFactory.CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<string>("SELECT PASSWORD_HASH FROM HR.USERS WHERE USERNAME = :Username", new { Username = username });
        }

        public async Task<int> CreateAsync(UserCreateRequest request, string passwordHash)
        {
            using var conn = _dbConnectionFactory.CreateConnection();
            var sql = @"INSERT INTO HR.USERS 
        (USERNAME, PASSWORD_HASH, ROLE, EMPLOYEE_ID, DEPARTMENT_ID, EMAIL, PHONE, USE_YN, CREATED_AT, DN)
        VALUES (:Username, :PasswordHash, :Role, :EmployeeId, :DepartmentId, :Email, :Phone, 'Y', SYSDATE, :Dn)";

            return await conn.ExecuteAsync(sql, new
            {
                request.Username,
                PasswordHash = passwordHash,
                request.Role,
                request.EmployeeId,
                request.DepartmentId,
                request.Email,
                request.Phone,
                request.Dn
            });
        }

        public async Task<int> DeleteAsync(int userId)
        {
            using var conn = _dbConnectionFactory.CreateConnection();
            return await conn.ExecuteAsync("DELETE FROM HR.USERS WHERE USER_ID = :UserId", new { UserId = userId });
        }
    }
}
