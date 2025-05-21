using CardinalProject.Models;

namespace CardinalProject.Repositories
{
    public interface IHospitalRepository
    {
        Task<IEnumerable<Hospital>> GetAllHospitalsAsync();
        Task<Hospital?> GetHospitalByIdAsync(int id);
        Task AddHospitalAsync(Hospital hospital);
        Task UpdateHospitalAsync(Hospital hospital);
        Task DeleteHospitalAsync(int id);
        Task<Hospital?> GetHospitalEntityByIdAsync(int id);
    }

}
