namespace fisio.domain.Repositories
{
    public interface IBaseRepository<T>
    {
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<T?> GetById(string id);
    }
}