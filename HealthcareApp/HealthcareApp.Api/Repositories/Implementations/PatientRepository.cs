using HealthcareApp.Api.Data;
using HealthcareApp.Api.Models.Entities;
using HealthcareApp.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HealthcareApp.Api.Repositories.Implementations
{
    public class PatientRepository : IPatientRepository
    {
        private readonly HospitalDbContext _context;

        public PatientRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Patient patient)
        {
            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Patient>> GetAllAsync(int hospitalId)
        {
            return await _context.Patients
                                 .Where(p => p.HospitalId == hospitalId)
                                 .ToListAsync();
        }

        public async Task<Patient?> GetByIdAsync(int id)
        {
            return await _context.Patients.FindAsync(id);
        }
        public async Task UpdatePatient(Patient patient)
        {
            _context.Patients.Update(patient);
            await _context.SaveChangesAsync();
        }
        public async Task DeletePatient(Patient patient)
        {
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
        }

    }
}
