using System;
using System.Collections.Generic;

namespace ReadLater5.DTO
{
    public class AuthResponse
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public DateTime Expires { get; set; }
    }
}
