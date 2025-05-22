using CardinalProject.Models;
using CardinalProject.Repositories;
using CardinalProject.ViewModels;

namespace CardinalProject.Services
{
    public class ServiceProviderService : IServiceProviderService
    {
        private readonly IServiceProviderRepository _serviceProviderRepository;

        public ServiceProviderService(IServiceProviderRepository serviceProviderRepository)
        {
            _serviceProviderRepository = serviceProviderRepository;
        }

        private ServiceProviderViewModel MapToViewModel(Models.ServiceProvider sp)
        {
            return new ServiceProviderViewModel
            {
                Id = sp.Id,
                Name = sp.Name,
                PostCode = sp.PostCode,
                Website = sp.Website,
                PhoneNumber = sp.PhoneNumber,
                IsActive = sp.IsActive,
                Services = sp.Services?
                    .Select(s => new ServiceViewModel
                    {
                        Id = s.Service.Id,
                        Name = s.Service.Name,
                        Description = s.Service.Description
                    }).ToList() ?? new List<ServiceViewModel>()
            };
        }

        private Models.ServiceProvider MapToModel(ServiceProviderViewModel vm)
        {
            return new Models.ServiceProvider
            {
                Id = vm.Id,
                Name = vm.Name,
                PostCode = vm.PostCode,
                Website = vm.Website,
                PhoneNumber = vm.PhoneNumber,
                IsActive = vm.IsActive,
            };
        }

    
        private string NormalizeUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return url;

            if (!url.StartsWith("http://", System.StringComparison.OrdinalIgnoreCase) &&
                !url.StartsWith("https://", System.StringComparison.OrdinalIgnoreCase))
            {
                return "http://" + url;
            }
            return url;
        }

        public async Task<IEnumerable<ServiceProviderViewModel>> GetAllServiceProvidersAsync()
        {
            var providers = await _serviceProviderRepository.GetAllServiceProvidersAsync();
            return providers.Select(MapToViewModel);
        }

        public async Task<ServiceProviderViewModel?> GetServiceProviderByIdAsync(int id)
        {
            var sp = await _serviceProviderRepository.GetServiceProviderByIdAsync(id);
            return sp == null ? null : MapToViewModel(sp);
        }

        public async Task AddServiceProviderAsync(ServiceProviderViewModel model, string createdBy)
        {
            var sp = MapToModel(model);

            sp.Website = NormalizeUrl(sp.Website);
            sp.CreatedAt = DateTime.UtcNow;
            sp.UpdatedAt = DateTime.UtcNow;
            sp.CreatedBy = createdBy;
            sp.UpdatedBy = createdBy;
            await _serviceProviderRepository.AddServiceProviderAsync(sp);
        }

        public async Task UpdateServiceProviderAsync(ServiceProviderViewModel model, string updatedBy)
        {
            var sp = await _serviceProviderRepository.GetServiceProviderByIdAsync(model.Id);
            if (sp == null) throw new Exception("Service Provider not found");

            sp.Name = model.Name;
            sp.PostCode = model.PostCode;
            sp.Website = NormalizeUrl(model.Website); 
            sp.PhoneNumber = model.PhoneNumber;
            sp.IsActive = model.IsActive;
            sp.UpdatedAt = DateTime.UtcNow;
            sp.UpdatedBy = updatedBy;

            await _serviceProviderRepository.UpdateServiceProviderAsync(sp);
        }

        public async Task DeleteServiceProviderAsync(int id)
        {
            await _serviceProviderRepository.DeleteServiceProviderAsync(id);
        }

        public async Task<bool> ServiceProviderExistsAsync(int id)
        {
            return await _serviceProviderRepository.ServiceProviderExistsAsync(id);
        }
    }
}
