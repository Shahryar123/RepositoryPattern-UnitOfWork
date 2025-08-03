using RepositoryPattern_And_UnitOfWork.Models;

namespace RepositoryPattern_And_UnitOfWork.Repository
{
    public interface IPlayersLevelRepository
    {
        Task<IEnumerable<PlayersLevel?>> GetAllAsync();
        Task<PlayersLevel?> GetByIdAsync(int id);
        Task<PlayersLevel> CreateAsync(PlayersLevel playersLevel);
        Task<bool> UpdateAsync(int id, PlayersLevel playersLevel);
        Task<bool> DeleteAsync(int id , PlayersLevel playersLevel);
    }
}
