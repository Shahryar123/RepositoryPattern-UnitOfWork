using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepositoryPattern_And_UnitOfWork.Data;
using RepositoryPattern_And_UnitOfWork.Models;

namespace RepositoryPattern_And_UnitOfWork.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayersLevelController : ControllerBase
    {
        private readonly AppDBContext _dbContext;
        public PlayersLevelController(AppDBContext dBContext) => _dbContext = dBContext;

        // GET: api/PlayersLevel
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayersLevel>>> GetAllAsync()
        {
            return await _dbContext.PlayersLevels.ToListAsync();
        }

        // GET: api/PlayersLevel/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PlayersLevel>> GetByIdAsync(int id)
        {
            var playerLevel = await _dbContext.PlayersLevels.FindAsync(id);
            if (playerLevel == null)
                return NotFound();
            return playerLevel;
        }

        // POST: api/PlayersLevel
        [HttpPost]
        public async Task<ActionResult<PlayersLevel>> CreateAsync([FromBody] PlayersLevel playersLevel)
        {
            _dbContext.PlayersLevels.Add(playersLevel);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetByIdAsync), new { id = playersLevel.Id }, playersLevel);
        }

        // PUT: api/PlayersLevel/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] PlayersLevel playersLevel)
        {
            if (id != playersLevel.Id)
                return BadRequest();

            _dbContext.Entry(playersLevel).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _dbContext.PlayersLevels.AnyAsync(e => e.Id == id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        // DELETE: api/PlayersLevel/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var playerLevel = await _dbContext.PlayersLevels.FindAsync(id);
            if (playerLevel == null)
                return NotFound();

            _dbContext.PlayersLevels.Remove(playerLevel);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
