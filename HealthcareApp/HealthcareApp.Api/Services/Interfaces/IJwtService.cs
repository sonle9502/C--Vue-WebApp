using HealthcareApp.Api.Models.Entities;

namespace HealthcareApp.Api.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
