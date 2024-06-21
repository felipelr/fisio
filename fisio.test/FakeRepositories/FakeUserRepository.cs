using fisio.domain.Entities;
using fisio.domain.Repositories;
using fisio.test.FakeContexts;
using Microsoft.EntityFrameworkCore;

namespace fisio.test.FakeRepositories
{
    internal class FakeUserRepository : FakeBaseRepository<User>, IUserRepository
    {
        private readonly FakeInMemoryContext _context;

        public FakeUserRepository(FakeInMemoryContext context) : base(context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.AsNoTracking()
                .Where(x => x.Active)
                .OrderBy(x => x.Email)
                .ToListAsync();
        }

        public async Task<User?> Login(string email, string password)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == email);

            if (user != null)
            {
                bool verified = BCrypt.Net.BCrypt.Verify(password, user.Password);
                if (verified)
                    return user;
            }

            return null;
        }
    }
}
