using Microsoft.EntityFrameworkCore;
using RepositoryPattern_And_UnitOfWork.Data;
using RepositoryPattern_And_UnitOfWork.Models;

namespace RepositoryPattern_And_UnitOfWork.Repository
{
    public class PlayersLevelRepository : IPlayersLevelRepository
    {
        private readonly AppDBContext _dbContext;
        public PlayersLevelRepository(AppDBContext dbContext) => _dbContext = dbContext;
        
        public Task<PlayersLevel> CreateAsync(PlayersLevel playersLevel)
        {
            _dbContext.PlayersLevels.Add(playersLevel);
            return _dbContext.SaveChangesAsync().ContinueWith(t => playersLevel);
        }

        public Task<bool> DeleteAsync(int id, PlayersLevel playersLevel)
        {
            _dbContext.PlayersLevels.Remove(playersLevel);
            return _dbContext.SaveChangesAsync().ContinueWith(t => true);
        }

        public Task<IEnumerable<PlayersLevel?>> GetAllAsync()
        {
            return _dbContext.PlayersLevels.ToListAsync().ContinueWith(t => (IEnumerable<PlayersLevel?>)t.Result);
        }

        public async Task<PlayersLevel?> GetByIdAsync(int id)
        {
            return await _dbContext.PlayersLevels.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(int id, PlayersLevel playersLevel)
        {
            var existingPlayerLevel = await _dbContext.PlayersLevels.FindAsync(id);
            if (existingPlayerLevel == null)
                return false;

            existingPlayerLevel.Name = playersLevel.Name;
            existingPlayerLevel.Level = playersLevel.Level;

            _dbContext.PlayersLevels.Update(existingPlayerLevel);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
