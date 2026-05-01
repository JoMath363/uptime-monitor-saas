using Api.Solution.Data;
using Api.Solution.Models;
using Api.Solution.Models.DTOs;
using Api.Solution.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Solution.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UnityOfWork _unityOfWork;
        private readonly UserService _userService;
        private readonly AuthService _authService;

        public AuthController(UnityOfWork unityOfWork, UserService userService, AuthService authService)
        {
            _unityOfWork = unityOfWork;
            _userService = userService;
            _authService = authService;
        }

        [HttpPost("/register")]
        public async Task<IActionResult> Register([FromBody] AuthRequest dto)
        {
            User newUser = new User()
            {
                Email = dto.Email,
                PasswordHash = _authService.HashPassword(dto.Password)
            };

            await _userService.CreateAsync(newUser);
            await _unityOfWork.SaveChangesAsync();

            var token = _authService.GenerateToken(newUser);

            return Ok(new { token });
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login([FromBody] AuthRequest dto)
        {
            var user = await _userService.GetByEmailAsync(dto.Email);

            if (user == null)
                return Unauthorized("No user found for this email.");

            if (!_authService.VerifyPassword(dto.Password, user.PasswordHash))
                return Unauthorized("Invalid password.");

            var token = _authService.GenerateToken(user);

            return Ok(new { token });
        }
    }
}
