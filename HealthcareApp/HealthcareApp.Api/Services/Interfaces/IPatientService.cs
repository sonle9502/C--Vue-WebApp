using HealthcareApp.Api.Models.Entities;

namespace HealthcareApp.Api.Services.Interfaces
{
    public interface IPatientService
    {
        Task<Patient?> GetPatient(int id);
        Task<IEnumerable<Patient>> GetAllPatients(int hospitalId);
        Task RegisterPatient(Patient patient);
        Task UpdatePatient(Patient patient);
        Task DeletePatient(Patient patient);
    }
}
