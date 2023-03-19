using System;
using Microsoft.EntityFrameworkCore;
using fisio.domain.Entities;
using fisio.infra.Contexts;
using fisio.domain.Repositories;

namespace fisio.infra.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly FisioInMemoryContext _context;

        public UserRepository(FisioInMemoryContext context) : base(context)
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