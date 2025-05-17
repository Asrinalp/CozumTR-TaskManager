using Microsoft.EntityFrameworkCore;
using TaskManager.DTOs;
using TaskManager.Models;

namespace TaskManager.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
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
    }
}
