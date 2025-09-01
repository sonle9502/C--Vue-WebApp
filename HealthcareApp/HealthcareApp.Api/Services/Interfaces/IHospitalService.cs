using HealthcareApp.Api.Models.Entities;
using System.Threading.Tasks;

namespace HealthcareApp.Api.Services.Interfaces
{
    public interface IHospitalService
    {
        Task<Hospital> CreateHospitalAsync(Hospital hospital);
        Task<Hospital?> GetHospitalAsync(int id);
        Task<IEnumerable<Hospital>> GetAllHospitalsAsync();
    }
}
