using fisio.domain.UnitOfWork;

namespace fisio.test.FakeUnitOfWorks
{
    internal class FakeUnitOfWork : IUnitOfWork
    {
        public Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
