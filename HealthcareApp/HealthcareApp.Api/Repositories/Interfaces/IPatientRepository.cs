using HealthcareApp.Api.Models.Entities;

namespace HealthcareApp.Api.Repositories.Interfaces
{
    public interface IPatientRepository
    {
        Task<Patient?> GetByIdAsync(int id);
        Task<IEnumerable<Patient>> GetAllAsync(int hospitalId);
        Task AddAsync(Patient patient);
        Task UpdatePatient(Patient patient);
        Task DeletePatient(Patient patient);
        
    }
}
