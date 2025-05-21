using CardinalProject.Models;
using CardinalProject.ViewModels;

namespace CardinalProject.Services
{
    public interface IHospitalService
    {
        Task<IEnumerable<HospitalViewModel>> GetAllHospitalsAsync();
        Task<HospitalViewModel?> GetHospitalByIdAsync(int id);
        Task AddHospitalAsync(HospitalViewModel model, string createdBy);
        Task UpdateHospitalAsync(HospitalViewModel model, string updatedBy);
        Task DeleteHospitalAsync(int hospitalId, string deletedBy);
        Task<Hospital?> GetHospitalEntityByIdAsync(int id);

    }
}
