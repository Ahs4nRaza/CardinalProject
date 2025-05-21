using CardinalProject.Models;
using CardinalProject.ViewModels;

public interface IUserRepository
{
    Task<User?> GetUserEntityByEmailAsync(string email);
    Task<User?> GetUserEntityByIdAsync(int userId);
    Task<UserViewModel?> GetUserByEmailAsync(string email);
    Task<IEnumerable<UserViewModel>> GetAllUsersAsync();
    Task<IEnumerable<UserViewModel>> GetAllUsersInHospitalAsync(int hospitalId);
    Task<IEnumerable<UserViewModel>> GetAllUsersInHospitalMinusAdminAsync(int hospitalId);
    Task<UserViewModel?> GetUserByIdAsync(int userId);
    Task AddUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(int userId);
    Task DeactivateUsersWhenHospitalDeletedAsync(int hospitalId, string updatedBy);
}
