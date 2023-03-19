namespace fisio.domain.Repositories
{
    public interface IBaseRepository<T>
    {
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task<T?> GetById(string id);
    }
}