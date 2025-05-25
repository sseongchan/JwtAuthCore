using JwtAuthSample.Modules.User.Dtos;
using JwtAuthSample.Modules.User.Repositories;
using Microsoft.AspNetCore.Identity;

namespace JwtAuthSample.Modules.User.Services
{
    public class UserService(IUserRepository userRepository)
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly PasswordHasher<UserCreateRequest> _passwordHasher = new();

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync() => await _userRepository.GetAllAsync();

        public async Task<UserDto?> GetUserByIdAsync(int userId) => await _userRepository.GetByIdAsync(userId);

        public async Task<int> CreateUserAsync(UserCreateRequest request)
        {
            var hash = _passwordHasher.HashPassword(request, request.Password);
            return await _userRepository.CreateAsync(request, hash);
        }

        public async Task<int> DeleteUserAsync(int userId) => await _userRepository.DeleteAsync(userId);

        public async Task<UserDto?> ValidateUserAsync(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            if (user == null) return null;

            var passwordHash = await _userRepository.GetPasswordHashAsync(username);
            var hasher = new PasswordHasher<UserDto>();
            var result = hasher.VerifyHashedPassword(user, passwordHash, password);

            return result == PasswordVerificationResult.Success ? user : null;
        }
    }
}
