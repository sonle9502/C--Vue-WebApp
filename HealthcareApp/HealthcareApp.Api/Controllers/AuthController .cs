using HealthcareApp.Api.Models.Entities;
using HealthcareApp.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _IJwtService;

        public AuthController(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _IJwtService = jwtService;
        }

        // POST: api/Auth/Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            var user = await _userService.Authenticate(request.Username, request.Password);
            if (user == null)
            {
                return Unauthorized("Username or password is incorrect.");
            }

            var token = _IJwtService.GenerateToken(user);
            return Ok(new { Token = token });
        }
        // POST: api/Auth/Register
        [HttpPost("Register")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest request)
        {
            // Kiểm tra username đã tồn tại chưa
            var existingUser = await _userService.GetByUsername(request.Username);
            if (existingUser != null)
            {
                return BadRequest("Username is already taken.");
            }

            // Tạo user mới
            var newUser = new User
            {
                UserName = request.Username,
                PasswordHash = _userService.HashPassword(request.Password),
                Role = "User", // hoặc role mặc định
                HospitalId = request.HospitalId
            };

            await _userService.CreateUser(newUser);

            return Ok("User registered successfully.");
        }
    }
}
