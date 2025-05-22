
namespace CardinalProject.Repositories
{
    public interface IServiceProviderRepository
    {
        Task<IEnumerable<Models.ServiceProvider>> GetAllServiceProvidersAsync();
        Task<Models.ServiceProvider?> GetServiceProviderByIdAsync(int id);
        Task AddServiceProviderAsync(Models.ServiceProvider serviceProvider);
        Task UpdateServiceProviderAsync(Models.ServiceProvider serviceProvider);
        Task DeleteServiceProviderAsync(int id);
        Task<bool> ServiceProviderExistsAsync(int id);
    }
}
