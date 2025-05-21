using CardinalProject.Models;

namespace CardinalProject.Services
{
    public interface IAuthService
    {
        Task<User?> ValidateUserCredentialsAsync(string email, string password);
    }
    
}
