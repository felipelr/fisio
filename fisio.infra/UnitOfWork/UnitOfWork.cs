using fisio.domain.UnitOfWork;
using fisio.infra.Contexts;

namespace fisio.infra.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FisioInMemoryContext _dbContext;
        public UnitOfWork(FisioInMemoryContext dbContext) {
            _dbContext = dbContext;
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
