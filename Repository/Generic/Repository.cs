using Microsoft.EntityFrameworkCore;
using RepositoryPattern_And_UnitOfWork.Data;

namespace RepositoryPattern_And_UnitOfWork.Repository.Generic
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDBContext _dbContext;
        private readonly DbSet<T> _dbSet;
        public Repository(AppDBContext dbContext) 
        {
            _dbSet = dbContext.Set<T>();
            _dbContext = dbContext; 
        }

        public async Task<IEnumerable<T?>> GetAllAsync()
        {
            return await _dbSet.ToListAsync().ContinueWith(t => (IEnumerable<T?>)t.Result);
        }
        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id).AsTask();
        }
        public async Task<T> CreateAsync(T entity)
        {
            _dbSet.Add(entity);
            return entity;
        }
        public async Task<bool> UpdateAsync(int id, T entity)
        {
            _dbSet.Update(entity);
            return await _dbContext.SaveChangesAsync().ContinueWith(t => true);
        }
        public async Task<bool> DeleteAsync(int id, T entity)
        {
            _dbSet.Remove(entity);
            return await _dbContext.SaveChangesAsync().ContinueWith(t => true);
        }
    }
}
