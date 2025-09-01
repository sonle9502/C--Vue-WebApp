using HealthcareApp.Api.Data;
using HealthcareApp.Api.Models.Entities;
using HealthcareApp.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HealthcareApp.Api.Services.Implementations
{
    public class HospitalRepository : IHospitalRepository
    {
        private readonly HospitalDbContext _context;

        public HospitalRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<Hospital> AddAsync(Hospital hospital)
        {
            _context.Hospitals.Add(hospital);
            await _context.SaveChangesAsync();
            return hospital;
        }

        public async Task<Hospital?> GetByIdAsync(int id)
        {
            return await _context.Hospitals.FindAsync(id);
        }
        public async Task<IEnumerable<Hospital>> GetAllAsync()
        {
            return await _context.Hospitals.ToListAsync();
        }
    }
}
