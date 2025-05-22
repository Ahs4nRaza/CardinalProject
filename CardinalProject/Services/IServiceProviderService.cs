using CardinalProject.ViewModels;

namespace CardinalProject.Services
{
    public interface IServiceProviderService
    {
        Task<IEnumerable<ServiceProviderViewModel>> GetAllServiceProvidersAsync();
        Task<ServiceProviderViewModel?> GetServiceProviderByIdAsync(int id);
        Task AddServiceProviderAsync(ServiceProviderViewModel model, string createdBy);
        Task UpdateServiceProviderAsync(ServiceProviderViewModel model, string updatedBy);
        Task DeleteServiceProviderAsync(int id);
        Task<bool> ServiceProviderExistsAsync(int id);
    }
}
