using fisio.domain.Entities;

namespace fisio.domain.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<IEnumerable<User>> GetAll();
        Task<User?> Login(string email, string password);
    }
}