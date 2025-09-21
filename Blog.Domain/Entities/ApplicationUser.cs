using Microsoft.AspNetCore.Identity;



namespace Blog.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? DisplayName { get; set; }
        public string? ProfileImageUrl { get; set; }
    }
}
