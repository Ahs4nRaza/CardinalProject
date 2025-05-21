using CardinalProject.Models;

namespace CardinalProject.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmailAsync(string email);
    }
}
