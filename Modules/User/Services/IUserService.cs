using JwtAuthSample.Modules.User.Dtos;

namespace JwtAuthSample.Modules.User.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto?> GetUserByIdAsync(int userId);
        Task<int> CreateUserAsync(UserCreateRequest request);
        Task<int> DeleteUserAsync(int userId);
        Task<UserDto?> ValidateUserAsync(string username, string password);
    }
}
