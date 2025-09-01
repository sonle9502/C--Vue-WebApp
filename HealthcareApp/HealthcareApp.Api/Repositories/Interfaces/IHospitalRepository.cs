using HealthcareApp.Api.Models.Entities;
using System.Threading.Tasks;

namespace HealthcareApp.Api.Services.Interfaces
{
    public interface IHospitalRepository
    {
        Task<Hospital> AddAsync(Hospital hospital);
        Task<Hospital?> GetByIdAsync(int id);
        Task<IEnumerable<Hospital>> GetAllAsync();
    }
}
