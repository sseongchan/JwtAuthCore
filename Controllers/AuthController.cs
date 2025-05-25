using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtAuthSample.Config;
using JwtAuthSample.Models;
using JwtAuthSample.Modules.User.Dtos;
using JwtAuthSample.Modules.User.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using LoginRequest = JwtAuthSample.Models.LoginRequest;

namespace JwtAuthSample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IOptions<JwtSettings> jwtSettings, IUserService userService) : ControllerBase
    {
        private readonly JwtSettings _jwtSettings = jwtSettings.Value;
        private readonly IUserService _userService = userService;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userService.ValidateUserAsync(request.Username, request.Password);
            if (user is null)
                return Unauthorized("Invalid credentials");

            var sessionId = Guid.NewGuid().ToString();
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim("role", user.Role),
            new Claim("dept", user.DepartmentId?.ToString() ?? "None"),
            new Claim("sessionId", sessionId)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityTokenHandler().CreateToken(new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = creds
            });

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expires = DateTime.UtcNow.AddHours(1),
                username = user.Username
            });
        }

        [Authorize]
        [HttpGet("profile")]
        public IActionResult Profile()
        {
            return Ok(new { user = User.Identity?.Name });
        }
    }
}
