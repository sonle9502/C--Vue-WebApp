using Microsoft.AspNetCore.Identity;

namespace HealthcareApp.Api.Models.Entities
{
    public class User : IdentityUser
    {
        public string Role { get; set; } = "User"; // Admin, Doctor, User...
        public int HospitalId { get; set; }
        public Hospital Hospital { get; set; } // navigation property
    }
    public class UserRegisterRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int HospitalId { get; set; }
        public string? Role { get; set; } = "User"; // Optional
    }
    // Model request đăng nhập
    public class UserLoginRequest
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
