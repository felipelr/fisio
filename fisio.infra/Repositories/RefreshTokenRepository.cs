using System;
using Microsoft.EntityFrameworkCore;
using fisio.domain.Entities;
using fisio.infra.Contexts;
using fisio.domain.Repositories;

namespace fisio.infra.Repositories
{
    public class RefreshTokenRepository : BaseRepository<RefreshToken>, IRefreshTokenRepository
    {
        private readonly FisioInMemoryContext _context;

        public RefreshTokenRepository(FisioInMemoryContext context) : base(context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public async Task<RefreshToken?> GetByKey(string key)
        {
            return await _context.RefreshTokens.AsNoTracking()
                .Where(x => x.TokenKey == key)
                .FirstOrDefaultAsync();
        }
    }
}