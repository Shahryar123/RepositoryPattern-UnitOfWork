namespace RepositoryPattern_And_UnitOfWork.Repository.Generic
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T?>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T> CreateAsync(T entity);
        Task<bool> UpdateAsync(int id, T entity);
        Task<bool> DeleteAsync(int id, T entity);
    }
    
}
