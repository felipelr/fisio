using fisio.domain.Entities;

namespace fisio.domain.Repositories
{
    public interface IPatientRepository : IBaseRepository<Patient>
    {
        Task<IEnumerable<Patient>> GetAll();
        Task<Patient?> GetByUser(string userId);
    }
}