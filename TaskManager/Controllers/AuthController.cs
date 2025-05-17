using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManager.DTOs;
using TaskManager.Models;
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
        public async Task<IActionResult> Login(UserDto dto)
        {
            var user = await _userService.AuthenticateAsync(dto);
            if (user == null) return Unauthorized("Invalid username or password");

            var token = CreateToken(user);
            return Ok(new { token });
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
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
