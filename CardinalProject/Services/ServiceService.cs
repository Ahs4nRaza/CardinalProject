using CardinalProject.Models;
using CardinalProject.Repositories;
using CardinalProject.ViewModels;

namespace CardinalProject.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;

        public ServiceService(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        private ServiceViewModel MapToViewModel(Service service) => new ServiceViewModel
        {
            Id = service.Id,
            Name = service.Name,
            Description = service.Description
        };

        private Service MapToModel(ServiceViewModel vm) => new Service
        {
            Id = vm.Id,
            Name = vm.Name,
            Description = vm.Description?.Trim()
        };

        public async Task<ServiceViewModel?> GetServiceByIdAsync(int id)
        {
            var service = await _serviceRepository.GetServiceByIdAsync(id);
            if (service == null) return null;

            return MapToViewModel(service);
        }

        public async Task<IEnumerable<ServiceViewModel>> GetAllServicesAsync()
        {
            var services = await _serviceRepository.GetAllServicesAsync();
            return services.Select(MapToViewModel);
        }

        public async Task AddServiceAsync(ServiceViewModel vm, string createdBy)
        {
            var service = MapToModel(vm);
            service.CreatedAt = DateTime.UtcNow;
            service.CreatedBy = createdBy;
            service.UpdatedAt = DateTime.UtcNow;
            service.UpdatedBy = createdBy;

            await _serviceRepository.AddServiceAsync(service);
        }

        public async Task UpdateServiceAsync(ServiceViewModel vm, string updatedBy)
        {
            var existingService = await _serviceRepository.GetServiceByIdAsync(vm.Id);
            if (existingService == null)
                throw new Exception("Service not found");

            existingService.Name = vm.Name;
            existingService.Description = vm.Description?.Trim();
            existingService.UpdatedAt = DateTime.UtcNow;
            existingService.UpdatedBy = updatedBy;

            await _serviceRepository.UpdateServiceAsync(existingService);
        }

        public async Task DeleteServiceAsync(int id)
        {
            await _serviceRepository.DeleteServiceAsync(id);
        }

        public async Task<bool> ServiceExistsAsync(int id)
        {
            return await _serviceRepository.ServiceExistsAsync(id);
        }
    }

}
