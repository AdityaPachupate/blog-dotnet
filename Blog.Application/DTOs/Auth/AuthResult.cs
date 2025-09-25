using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.DTOs.Auth
{
    public class AuthResult
    {
        public bool Success { get; set; }
        public string Token { get; set; } = string.Empty;
        public string[] Errors { get; set; } = Array.Empty<string>();
        public UserDto? User { get; set; }
    }
}
