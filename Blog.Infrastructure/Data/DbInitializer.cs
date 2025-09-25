using Microsoft.AspNetCore.Identity;
using Blog.Domain.Entities;
using Blog.Infrastructure.Persistence;

namespace Blog.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(BlogDbContext context, UserManager<User> userManager)
        {
            // Ensure database is created
            await context.Database.EnsureCreatedAsync();

            // Check if we already have users
            if (userManager.Users.Any()) return;

            // Create a sample user
            var sampleUser = new User
            {
                UserName = "author@example.com",
                Email = "author@example.com",
                FirstName = "John",
                LastName = "Doe",
                Bio = "Sample author for testing purposes",
                EmailConfirmed = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await userManager.CreateAsync(sampleUser, "Password123!");

            await context.SaveChangesAsync();
        }
    }
} 