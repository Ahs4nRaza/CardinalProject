using CardinalProject.Models;
using CardinalProject.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CardinalProject.Services
{
    public class HospitalService : IHospitalService
    {
        private readonly IHospitalRepository _hospitalRepository;

        public HospitalService(IHospitalRepository hospitalRepository)
        {
            _hospitalRepository = hospitalRepository;
        }

        public async Task<IEnumerable<Hospital>> GetAllHospitalsAsync()
        {
            return await _hospitalRepository.GetAllHospitalsAsync();
        }

        public async Task<Hospital?> GetHospitalByIdAsync(int id)
        {
            return await _hospitalRepository.GetHospitalByIdAsync(id);
        }
    }
}
