using CardinalProject.Models;

namespace CardinalProject.Repositories
{
    public interface IServiceRepository
    {
        Task<Service?> GetServiceByIdAsync(int id);
        Task<IEnumerable<Service>> GetAllServicesAsync();
        Task AddServiceAsync(Service service);
        Task UpdateServiceAsync(Service service);
        Task DeleteServiceAsync(int id);
        Task<bool> ServiceExistsAsync(int id);
    }

}
