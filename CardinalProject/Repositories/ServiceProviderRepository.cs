using CardinalProject.Data;
using Microsoft.EntityFrameworkCore;

namespace CardinalProject.Repositories
{
    public class ServiceProviderRepository : IServiceProviderRepository
    {
        private readonly ApplicationDbContext _context;

        public ServiceProviderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.ServiceProvider>> GetAllServiceProvidersAsync()
        {
           
            return await _context.ServiceProviders.ToListAsync();
        }

        public async Task<Models.ServiceProvider?> GetServiceProviderByIdAsync(int id)
        {
            return await _context.ServiceProviders
                .FirstOrDefaultAsync(sp => sp.Id == id);
        }

        public async Task AddServiceProviderAsync(Models.ServiceProvider serviceProvider)
        {
            await _context.ServiceProviders.AddAsync(serviceProvider);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateServiceProviderAsync(Models.ServiceProvider serviceProvider)
        {
            _context.ServiceProviders.Update(serviceProvider);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteServiceProviderAsync(int id)
        {
            var sp = await _context.ServiceProviders.FindAsync(id);
            if (sp != null)
            {
                _context.ServiceProviders.Remove(sp);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ServiceProviderExistsAsync(int id)
        {
            return await _context.ServiceProviders.AnyAsync(sp => sp.Id == id);
        }
    }
}
