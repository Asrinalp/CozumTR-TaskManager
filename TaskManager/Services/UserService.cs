using Microsoft.EntityFrameworkCore;
using TaskManager.DTOs;
using TaskManager.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TaskManager.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public UserService(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }


        public async Task<User?> AuthenticateAsync(UserDto dto)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Username == dto.Username && u.Password == dto.Password);
        }

        public async Task<bool> RegisterAsync(UserDto dto)
        {
            var exists = await _context.Users.AnyAsync(u => u.Username == dto.Username);
            if (exists) return false;

            var user = new User
            {
                Username = dto.Username,
                Password = dto.Password
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }
        public string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
        new Claim(ClaimTypes.Name, user.Username),
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
    };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? throw new InvalidOperationException("Jwt:Invalid Key"))
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
