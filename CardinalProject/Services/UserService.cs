using CardinalProject.Models;
using CardinalProject.Repositories;
using CardinalProject.ViewModels;

namespace CardinalProject.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User?> GetUserEntityByIdAsync(int userId)
        {
            return await _userRepository.GetUserEntityByIdAsync(userId);
        }
        public async Task<IEnumerable<UserViewModel>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<IEnumerable<UserViewModel>> GetAllUsersInHospitalAsync(int hospitalId)
        {
            return await _userRepository.GetAllUsersInHospitalAsync(hospitalId);
        }

        public async Task<IEnumerable<UserViewModel>> GetAllUsersInHospitalMinusAdminAsync(int hospitalId)
        {
            return await _userRepository.GetAllUsersInHospitalMinusAdminAsync(hospitalId);
        }

        public async Task<UserViewModel?> GetUserByIdAsync(int userId)
        {
            return await _userRepository.GetUserByIdAsync(userId);
        }

        public async Task AddUserAsync(User user, string createdBy)
        {
            
            user.CreatedAt = DateTime.UtcNow;
            user.CreatedBy = createdBy;
            user.UpdatedAt = DateTime.UtcNow;
            user.UpdatedBy = createdBy;

            await _userRepository.AddUserAsync(user);
        }

        public async Task UpdateUserAsync(User user, string updatedBy)
        {
            user.UpdatedAt = DateTime.UtcNow;
            user.UpdatedBy = updatedBy;

            await _userRepository.UpdateUserAsync(user);
        }

        public async Task DeleteUserAsync(int userId)
        {
            await _userRepository.DeleteUserAsync(userId);
        }
    }
}
