using CardinalProject.Models;
namespace CardinalProject.Services
{
    public interface IHospitalService
    {
        Task<IEnumerable<Hospital>> GetAllHospitalsAsync();
        Task<Hospital?> GetHospitalByIdAsync(int id);
    }
}
