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

        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<T?> GetById(string id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
    }
}
