using HealthcareApp.Api.Models.Entities;

namespace HealthcareApp.Api.Services.Interfaces
{
    public interface IUserService
    {
        Task<User?> Authenticate(string username, string password);
        Task<User> Register(User user, string password);
        Task<User?> GetById(int id);
        Task<User?> GetByUsername(string username);
        Task<bool> ValidateUser(string username, string password);
        string HashPassword(string password);
        Task<User> CreateUser(User user);
    }
}
