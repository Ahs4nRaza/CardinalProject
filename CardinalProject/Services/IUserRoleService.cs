using CardinalProject.Models;

namespace CardinalProject.Services
{
    public interface IUserRoleService
    {
        Task<IEnumerable<UserRole>> GetAllUserRolesAsync();
        Task<UserRole?> GetUserRoleByIdAsync(int id);
    }
}
