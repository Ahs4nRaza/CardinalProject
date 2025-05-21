using CardinalProject.Models;
using CardinalProject.Repositories;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace CardinalProject.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _userroleRepository;

        public UserRoleService(IUserRoleRepository roleRepository)
        {
            _userroleRepository = roleRepository;
        }

        public async Task<IEnumerable<UserRole>> GetAllUserRolesAsync()
        {
            return await _userroleRepository.GetAllUserRolesAsync();
        }

        public async Task<UserRole?> GetUserRoleByIdAsync(int id)
        {
            return await _userroleRepository.GetUserRoleByIdAsync(id);
        }
    }
}
