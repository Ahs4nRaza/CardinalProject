using CardinalProject.Models;

namespace CardinalProject.Repositories
{
    public interface IHospitalRepository
    {
        Task<IEnumerable<Hospital>> GetAllHospitalsAsync();
        Task<Hospital?> GetHospitalByIdAsync(int id);
    }
}
