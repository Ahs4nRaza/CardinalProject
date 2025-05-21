using CardinalProject.Models;

namespace CardinalProject.Repositories
{
    public interface IUserRoleRepository
    {
        Task<IEnumerable<UserRole>> GetAllUserRolesAsync();
        Task<UserRole?> GetUserRoleByIdAsync(int id);
    }
}
