using HealthcareApp.Api.Models.Entities;
using HealthcareApp.Api.Repositories.Interfaces;
using HealthcareApp.Api.Services.Interfaces;

namespace HealthcareApp.Api.Services.Implementations
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _repository;

        public PatientService(IPatientRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Patient>> GetAllPatients(int hospitalId)
        {
            return await _repository.GetAllAsync(hospitalId);
        }

        public async Task<Patient?> GetPatient(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task RegisterPatient(Patient patient)
        {
            await _repository.AddAsync(patient);
        }

        public async Task UpdatePatient(Patient patient)
        {
            var existing = await _repository.GetByIdAsync(patient.Id);
            if (existing == null) throw new Exception("Patient not found");

            // Cập nhật từng trường
            existing.Name = patient.Name;
            existing.Age = patient.Age;
            existing.Address = patient.Address;
            existing.Gender = patient.Gender;
            existing.BloodType = patient.BloodType;

            await _repository.UpdatePatient(existing);
        }
        public async Task DeletePatient(Patient patient)
        {
            var existing = await _repository.GetByIdAsync(patient.Id);
            if (existing == null) throw new Exception("Patient not found");

            await _repository.DeletePatient(existing);
        }

    }
}
