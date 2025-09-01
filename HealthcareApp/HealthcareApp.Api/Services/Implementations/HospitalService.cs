using HealthcareApp.Api.Models.Entities;
using HealthcareApp.Api.Services.Interfaces;
using System.Threading.Tasks;

namespace HealthcareApp.Api.Services.Implementations
{
    public class HospitalService : IHospitalService
    {
        private readonly IHospitalRepository _repository;

        public HospitalService(IHospitalRepository repository)
        {
            _repository = repository;
        }

        public async Task<Hospital> CreateHospitalAsync(Hospital hospital)
        {
            // logic validate nếu cần
            return await _repository.AddAsync(hospital);
        }

        public async Task<Hospital?> GetHospitalAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task<IEnumerable<Hospital>> GetAllHospitalsAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}
