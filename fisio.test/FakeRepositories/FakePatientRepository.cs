using fisio.domain.Entities;
using fisio.domain.Repositories;
using fisio.test.FakeContexts;
using Microsoft.EntityFrameworkCore;

namespace fisio.test.FakeRepositories
{
    internal class FakePatientRepository : FakeBaseRepository<Patient>, IPatientRepository
    {
        private readonly FakeInMemoryContext _context;

        public FakePatientRepository(FakeInMemoryContext context) : base(context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public async Task<Patient?> GetByUser(string userId)
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
