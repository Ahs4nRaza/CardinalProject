using CardinalProject.Data;
using CardinalProject.Models;
using Microsoft.EntityFrameworkCore;


namespace CardinalProject.Repositories
{
    public class HospitalRepository : IHospitalRepository
    {
        private readonly ApplicationDbContext _context;

        public HospitalRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Hospital>> GetAllHospitalsAsync()
        {
            return await _context.Hospitals.AsNoTracking().ToListAsync();
        }

        public async Task<Hospital?> GetHospitalByIdAsync(int id)
        {
            return await _context.Hospitals.AsNoTracking().FirstOrDefaultAsync(h => h.Id == id);
        }
    }
}
