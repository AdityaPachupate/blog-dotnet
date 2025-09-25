using System.ComponentModel.DataAnnotations;

namespace Blog.API.Models.Auth
{
	public class RegisterRequest
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; } = string.Empty;

		[Required]
		[StringLength(100, MinimumLength = 6)]
		public string Password { get; set; } = string.Empty;

		[Required]
		[StringLength(50, MinimumLength = 1)]
		public string FirstName { get; set; } = string.Empty;

		[Required]
		[StringLength(50, MinimumLength = 1)]
		public string LastName { get; set; } = string.Empty;
	}
} 