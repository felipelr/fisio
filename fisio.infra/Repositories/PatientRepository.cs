using System;
using Microsoft.EntityFrameworkCore;
using fisio.domain.Entities;
using fisio.infra.Contexts;
using fisio.domain.Repositories;

namespace fisio.infra.Repositories
{
    public class PatientRepository : BaseRepository<Patient>, IPatientRepository
    {
        private readonly FisioInMemoryContext _context;

        public PatientRepository(FisioInMemoryContext context) : base(context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public async  Task<Patient?> GetByUser(string userId)
        {
            return await _context.Patients.AsNoTracking()
                .Where(x => x.UserId == userId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Patient>> GetAll()
        {
            return await _context.Patients.AsNoTracking()
                .Where(x => x.Active)
                .OrderBy(x => x.Name)
                .ToListAsync();
        }
    }
}