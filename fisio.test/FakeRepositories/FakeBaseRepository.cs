using Microsoft.EntityFrameworkCore;
using fisio.domain.Repositories;
using fisio.test.FakeContexts;

namespace fisio.test.FakeRepositories
{
    internal class FakeBaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly FakeInMemoryContext _context;

        public FakeBaseRepository(FakeInMemoryContext context)
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
