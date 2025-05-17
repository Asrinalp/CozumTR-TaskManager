using TaskManager.DTOs;
using TaskManager.Models;

namespace TaskManager.Services
{
    public interface IUserService
    {
        Task<User?> AuthenticateAsync(UserDto dto);
        Task<bool> RegisterAsync(UserDto dto);
        string GenerateJwtToken(User user);

    }
}
