using Blog.Application.DTOs.Auth;
using Blog.Application.Interfaces;
using Blog.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Services
{

    public class AuthService(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IConfiguration configuration) : IAuthService
    {
        public async  Task<string> GenerateJwtTokenAsync(User user)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");

            var claims = new List<Claim> { 
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName)
            };

            // Add user roles to claims
            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(int.Parse(jwtSettings["DurationInMinutes"]!)),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<AuthResult> LoginUserAsync(LoginUserDto request)
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return new AuthResult
                {
                    Success = false,
                    Errors = new[] { "Invalid login attempt." }
                };
            }

            var result = await signInManager
                .CheckPasswordSignInAsync(user,password: request.Password, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                var token = await GenerateJwtTokenAsync(user);
                return new AuthResult
                {
                    Success = true,
                    Token = token,
                    User = new UserDto
                    {
                        Id = user.Id,
                        Email = user.Email!,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        ProfilePictureUrl = user.ProfilePictureUrl
                    }
                };
            }

            return new AuthResult
            {
                Success = false,
                Errors = result.IsLockedOut
                    ? new[] { "Account is locked out" }
                    : new[] { "Invalid email or password" }
            };
        }

        public async Task<AuthResult> RegisterUserAsync(RegisterUserDto request)
        {
            var user = new User
            {
                UserName = request.Email,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var result = await userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                var token = await GenerateJwtTokenAsync(user);
                return new AuthResult
                {
                    Success = true,
                    Token = token,
                    User = new UserDto
                    {
                        Id = user.Id,
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        ProfilePictureUrl = user.ProfilePictureUrl
                    }
                };
            }

            return new AuthResult
            {
                Success = false,
                Errors = result.Errors.Select(e => e.Description).ToArray()
            };
        }
    }
}
