using CardinalProject.Models;
using CardinalProject.ViewModels;

public interface IUserService
{
    Task<User?> GetUserEntityByIdAsync(int userId);
    Task<IEnumerable<UserViewModel>> GetAllUsersAsync();
    Task<IEnumerable<UserViewModel>> GetAllUsersInHospitalAsync(int hospitalId);
    Task<IEnumerable<UserViewModel>> GetAllUsersInHospitalMinusAdminAsync(int hospitalId);
    Task<UserViewModel?> GetUserByIdAsync(int userId);
    Task AddUserAsync(User user, string createdBy);
    Task UpdateUserAsync(User user, string updatedBy);
    Task DeleteUserAsync(int userId);
}
