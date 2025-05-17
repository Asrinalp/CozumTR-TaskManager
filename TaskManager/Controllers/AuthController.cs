using Microsoft.AspNetCore.Mvc;
using TaskManager.DTOs;
using TaskManager.Services;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUserService _userService;

        public AuthController(IConfiguration config, IUserService userService)
        {
            _config = config;
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto dto)
        {
            var success = await _userService.RegisterAsync(dto);
            if (!success) return BadRequest("Username already exists");
            return Ok("User registered successfully");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDto dto)
        {
            var user = await _userService.AuthenticateAsync(dto);
            if (user == null) return Unauthorized("Invalid username or password");

            var token = _userService.GenerateJwtToken(user);
            return Ok(new { token });
        }

    }
}
