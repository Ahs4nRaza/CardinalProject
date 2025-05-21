using CardinalProject.Data;
using CardinalProject.Models;
using Microsoft.EntityFrameworkCore;


namespace CardinalProject.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserRole>> GetAllUserRolesAsync()
        {
            return await _context.UserRoles.AsNoTracking().ToListAsync();
        }

        public async Task<UserRole?> GetUserRoleByIdAsync(int id)
        {
            return await _context.UserRoles.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
