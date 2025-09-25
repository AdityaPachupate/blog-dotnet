using Microsoft.AspNetCore.Mvc;
using Blog.API.Models.Auth;
using Blog.Application.Interfaces;
using Blog.Application.DTOs.Auth;

namespace Blog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResult>> Register([FromBody] RegisterRequest request)
        {
            _logger.LogInformation("User registration attempt for email: {Email}", request.Email);

            var registerDto = new RegisterUserDto
            {
                Email = request.Email,
                Password = request.Password,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            var result = await _authService.RegisterUserAsync(registerDto);

            if (result.Success)
            {
                _logger.LogInformation("User registered successfully: {Email}", request.Email);
                return Ok(result);
            }

            _logger.LogWarning("User registration failed for email: {Email}. Errors: {Errors}", 
                request.Email, string.Join(", ", result.Errors));

            return BadRequest(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResult>> Login([FromBody] LoginRequest request)
        {
            _logger.LogInformation("Login attempt for email: {Email}", request.Email);

            var loginDto = new LoginUserDto
            {
                Email = request.Email,
                Password = request.Password
            };

            var result = await _authService.LoginUserAsync(loginDto);

            if (result.Success)
            {
                _logger.LogInformation("User logged in successfully: {Email}", request.Email);
                return Ok(result);
            }

            _logger.LogWarning("Login failed for email: {Email}. Errors: {Errors}", 
                request.Email, string.Join(", ", result.Errors));

            return BadRequest(result);
        }
    }
} 