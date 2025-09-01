using HealthcareApp.Api.Data;
using HealthcareApp.Api.Models.Entities;
using HealthcareApp.Api.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HealthcareApp.Api.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly HospitalDbContext _context;
        private readonly PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

        public UserService(HospitalDbContext context, IConfiguration configuration)
        {
            _context = context;
        }

        public async Task<User?> Authenticate(string username, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserName == username);
            if (user == null) return null;

            // So sánh password với hash đã lưu
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            if (result != PasswordVerificationResult.Success) return null;

            return user;
        }

        public async Task<User> Register(User user, string password)
        {
            user.PasswordHash = _passwordHasher.HashPassword(user, password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> GetById(int id)
        {
            return await _context.Users.FindAsync(id);
        }
        public async Task<User?> GetByUsername(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }
        public async Task<bool> ValidateUser(string username, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserName == username);
            if (user == null) return false;
            return user.PasswordHash == password; // TODO: hash compare
        }
        public string HashPassword(string password)
        {
            return _passwordHasher.HashPassword(new User(), password);
        }
        public async Task<User> CreateUser(User user)
        {
            // lưu user vào database, ví dụ _context.Users.Add(user) và await _context.SaveChangesAsync()
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

    }
}
