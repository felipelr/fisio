using fisio.domain.Entities;

namespace fisio.domain.Repositories
{
    public interface IRefreshTokenRepository : IBaseRepository<RefreshToken>
    {
        Task<RefreshToken?> GetByKey(string key);
    }
}