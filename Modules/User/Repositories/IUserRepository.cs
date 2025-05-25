using JwtAuthSample.Modules.User.Dtos;

namespace JwtAuthSample.Modules.User.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto?> GetByIdAsync(int userId);
        Task<int> CreateAsync(UserCreateRequest request, string passwordHash);
        Task<int> DeleteAsync(int userId);
        Task<UserDto?> GetByUsernameAsync(string username);
        Task<string?> GetPasswordHashAsync(string username);
    }
}
