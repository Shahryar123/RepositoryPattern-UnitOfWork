using Microsoft.EntityFrameworkCore.Storage;
using RepositoryPattern_And_UnitOfWork.Data;
using RepositoryPattern_And_UnitOfWork.Repository.Generic;
using System.Data;

namespace RepositoryPattern_And_UnitOfWork.Repository.UnitOfWork
{
    public class UnitofWork : IUnitofWork
    {
        private readonly AppDBContext _dBContext;
        private IDbContextTransaction _transaction;

        private readonly Dictionary<Type, object> _repositories = new();

        public UnitofWork(AppDBContext dBContext) 
        {
            _dBContext = dBContext ?? throw new ArgumentNullException(nameof(dBContext));
            _repositories = new Dictionary<Type, object>();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _dBContext.Database.BeginTransactionAsync();
        }
        
        public Task<int> SaveChangesAsync()
        {
            // Implementation for saving changes asynchronously
            return _dBContext.SaveChangesAsync();
        }
        public async Task CommitTransactionAsync()
        {
            try
            {
                await _transaction.CommitAsync();
            }
            catch
            {
                await _transaction.RollbackAsync();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }
        public async Task DeleteTransactionAsync()
        {
            // Implementation for deleting a transaction asynchronously
            if (_transaction == null)
            {
                throw new InvalidOperationException("Transaction has not been started.");
            }
             await _transaction.RollbackAsync();
             await _transaction.DisposeAsync();
            _transaction = null;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;
        public void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _transaction?.Dispose();
                    _dBContext?.Dispose();
                }
            }
            this.disposed = true;
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            var type = typeof(T);
            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<T>);
                var repositoryInstance = Activator.CreateInstance(repositoryType, _dBContext);
                _repositories[type] = repositoryInstance;
            }
            return (IRepository<T>)_repositories[type];
        }

    }
}
