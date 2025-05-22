using CardinalProject.ViewModels;

namespace CardinalProject.Services
{
    public interface IServiceService
    {
        Task<ServiceViewModel?> GetServiceByIdAsync(int id);
        Task<IEnumerable<ServiceViewModel>> GetAllServicesAsync();
        Task AddServiceAsync(ServiceViewModel serviceViewModel, string createdBy);
        Task UpdateServiceAsync(ServiceViewModel serviceViewModel, string updatedBy);
        Task DeleteServiceAsync(int id);
        Task<bool> ServiceExistsAsync(int id);
    }

}
