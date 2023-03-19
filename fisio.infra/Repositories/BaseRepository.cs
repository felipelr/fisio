using Microsoft.EntityFrameworkCore;
using fisio.infra.Contexts;
using fisio.domain.Repositories;

namespace fisio.infra.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly FisioInMemoryContext _context;

        public BaseRepository(FisioInMemoryContext context)
        {
            _context = context;
        }

        public async Task Create(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T?> GetById(string id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
    }
}