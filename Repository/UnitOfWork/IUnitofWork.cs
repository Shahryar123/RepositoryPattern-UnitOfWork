using RepositoryPattern_And_UnitOfWork.Repository.Generic;

namespace RepositoryPattern_And_UnitOfWork.Repository.UnitOfWork
{
    public interface IUnitofWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : class;
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task DeleteTransactionAsync();

    }
}
