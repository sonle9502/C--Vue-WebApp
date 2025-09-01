using HealthcareApp.Api.Data;
using HealthcareApp.Api.Models.Entities.lab;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.EntityFrameworkCore;

namespace HealthcareApp.Api.Repositories.lab
{
    public class LabRepository : ILabRepository
    {
        private readonly HospitalDbContext _context;
        public LabRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task SaveLabResultAsync(LabResult result)
        {
            // Kiểm tra patient tồn tại
            var patient = await _context.Patients.FindAsync(result.PatientId);
            if (patient == null)
            {
                throw new ArgumentException($"PatientId {result.PatientId} không tồn tại");
            }

            // Gán navigation property
            result.Patient = patient;

            _context.LabResults.Add(result);
            await _context.SaveChangesAsync();
        }
        public async Task<List<LabResult>> GetResultsAsync()
        {
            return await _context.LabResults
                                 .OrderByDescending(r => r.CreatedAt)
                                 .ToListAsync();
        }
    }

    public interface ILabRepository
    {
        Task SaveLabResultAsync(LabResult labResult);
        Task<List<LabResult>> GetResultsAsync();
    }
}
