using CardinalProject.Data;
using CardinalProject.Models;
using CardinalProject.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CardinalProject.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<User?> GetUserEntityByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<User?> GetUserEntityByIdAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }
        public async Task<UserViewModel?> GetUserByEmailAsync(string email)
        {
            var user = await _context.Users
                .Include(u => u.Hospital)
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null) return null;

            return MapToViewModel(user);
        }

        public async Task<IEnumerable<UserViewModel>> GetAllUsersAsync()
        {
            var users = await _context.Users
                .Include(u => u.Hospital)
                .Include(u => u.Role)
                .ToListAsync();

            return users.Select(u => MapToViewModel(u));
        }

        public async Task<IEnumerable<UserViewModel>> GetAllUsersInHospitalAsync(int hospitalId)
        {
            var users = await _context.Users
                .Where(u => u.HospitalId == hospitalId)
                .Include(u => u.Hospital)
                .Include(u => u.Role)
                .ToListAsync();

            return users.Select(u => MapToViewModel(u));
        }

        public async Task<IEnumerable<UserViewModel>> GetAllUsersInHospitalMinusAdminAsync(int hospitalId)
        {
            var users = await _context.Users
                .Where(u => u.HospitalId == hospitalId && u.RoleId != 3)
                .Include(u => u.Hospital)
                .Include(u => u.Role)
                .ToListAsync();

            return users.Select(u => MapToViewModel(u));
        }

        public async Task<UserViewModel?> GetUserByIdAsync(int userId)
        {
            var user = await _context.Users
                .Include(u => u.Hospital)
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null) return null;

            return MapToViewModel(user);
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
        public async Task DeactivateUsersWhenHospitalDeletedAsync(int hospitalId, string updatedBy)
        {
            var users = await _context.Users.Where(u => u.HospitalId == hospitalId && u.IsActive).ToListAsync();

            foreach (var user in users)
            {
                user.IsActive = false;
                user.UpdatedAt = DateTime.UtcNow;
                user.UpdatedBy = updatedBy;
            }

            await _context.SaveChangesAsync();
        }


        // Helper method to map User to UserViewModel
        private static UserViewModel MapToViewModel(User user)
        {
            return new UserViewModel
            {
                Id = user.Id,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                Department = user.Department ?? "",
                HospitalId = user.HospitalId ?? 0,
                HospitalName = user.Hospital?.Name ?? "",
                RoleId = user.RoleId,
                RoleName = user.Role?.Name ?? "",
                Email = user.Email,
                IsActive = user.IsActive
            };
        }
    }
}
