// Blog.Application/Interfaces/IAuthService.cs
using Blog.Application.DTOs.Auth;
using Blog.Domain.Entities;

namespace Blog.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResult> RegisterUserAsync(RegisterUserDto request);
        Task<AuthResult> LoginUserAsync(LoginUserDto request);
        Task<string> GenerateJwtTokenAsync(User user);
    }
}