using CardinalProject.Models;
using CardinalProject.Repositories;
using CardinalProject.ViewModels;

namespace CardinalProject.Services
{
    public class HospitalService : IHospitalService
    {
        private readonly IHospitalRepository _hospitalRepository;
        private readonly IUserRepository _userRepository;

        public HospitalService(IHospitalRepository hospitalRepository, IUserRepository userRepository)
        {
            _hospitalRepository = hospitalRepository;
            _userRepository = userRepository;
        }
        public async Task<Hospital?> GetHospitalEntityByIdAsync(int id)
        {
            return await _hospitalRepository.GetHospitalEntityByIdAsync(id);
        }


        public async Task<IEnumerable<HospitalViewModel>> GetAllHospitalsAsync()
        {
            var hospitals = await _hospitalRepository.GetAllHospitalsAsync();
            return hospitals.Select(h => MapToViewModel(h));
        }

        public async Task<HospitalViewModel?> GetHospitalByIdAsync(int id)
        {
            var hospital = await _hospitalRepository.GetHospitalByIdAsync(id);
            if (hospital == null) return null;
            return MapToViewModel(hospital);
        }

        public async Task AddHospitalAsync(HospitalViewModel model, string createdBy)
        {
            var hospital = MapToEntity(model);
            hospital.CreatedAt = DateTime.UtcNow;
            hospital.CreatedBy = createdBy;
            hospital.UpdatedAt = DateTime.UtcNow;
            hospital.UpdatedBy = createdBy;
            await _hospitalRepository.AddHospitalAsync(hospital);
        }

        public async Task UpdateHospitalAsync(HospitalViewModel model, string updatedBy)
        {
            var hospital = await GetHospitalEntityByIdAsync(model.Id);
            if (hospital == null)
                throw new Exception("Hospital not found.");

            hospital.Name = model.Name;
            hospital.PhoneNumber = model.PhoneNumber;
            hospital.IsActive = model.IsActive;
            hospital.UpdatedAt = DateTime.UtcNow;
            hospital.UpdatedBy = updatedBy;

            await _hospitalRepository.UpdateHospitalAsync(hospital);
        }

        public async Task DeleteHospitalAsync(int id, string deletedBy)
        {
            await _userRepository.DeactivateUsersWhenHospitalDeletedAsync(id, deletedBy);
            await _hospitalRepository.DeleteHospitalAsync(id);
        }

        // Mapping Helpers

        private HospitalViewModel MapToViewModel(Hospital hospital) => new HospitalViewModel
        {
            Id = hospital.Id,
            Name = hospital.Name,
            PhoneNumber = hospital.PhoneNumber,
            IsActive = hospital.IsActive
        };

        private static Hospital MapToEntity(HospitalViewModel model) => new Hospital
        {
            Id = model.Id,
            Name = model.Name,
            PhoneNumber = model.PhoneNumber,
            IsActive = model.IsActive
        };
    }
}
