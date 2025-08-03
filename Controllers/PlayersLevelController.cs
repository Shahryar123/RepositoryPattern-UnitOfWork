using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepositoryPattern_And_UnitOfWork.Data;
using RepositoryPattern_And_UnitOfWork.Models;
using RepositoryPattern_And_UnitOfWork.Repository;

namespace RepositoryPattern_And_UnitOfWork.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayersLevelController : ControllerBase
    {
        private readonly IPlayersLevelRepository _iplayers;
        public PlayersLevelController(IPlayersLevelRepository players) => _iplayers = players;

        // GET: api/PlayersLevel
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayersLevel?>>> GetAllAsync()
        {
            return await _iplayers.GetAllAsync() switch
            {
                IEnumerable<PlayersLevel> playersLevels => Ok(playersLevels),
                _ => NotFound()
            };
        }

        // GET: api/PlayersLevel/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PlayersLevel>> GetByIdAsync(int id)
        {
            var playersLevel = await _iplayers.GetByIdAsync(id);
            if (playersLevel == null)
                return NotFound();
            return Ok(playersLevel);
        }

        // POST: api/PlayersLevel
        [HttpPost]
        public async Task<ActionResult<PlayersLevel>> CreateAsync([FromBody] PlayersLevel playersLevel)
        {
            if (playersLevel == null)
                return BadRequest("PlayersLevel cannot be null");
            var createdPlayerLevel = await _iplayers.CreateAsync(playersLevel);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = createdPlayerLevel.Id }, createdPlayerLevel);
        }

        // PUT: api/PlayersLevel/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] PlayersLevel playersLevel)
        {
            var existingPlayerLevel = await _iplayers.GetByIdAsync(id);
            if (existingPlayerLevel == null)
                return NotFound();

            var updated = await _iplayers.UpdateAsync(id, playersLevel);
            if (!updated)
                return StatusCode(500, "PlayersLevel data is Empty");
            return NoContent();
        }

        // DELETE: api/PlayersLevel/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var existingPlayerLevel = await _iplayers.GetByIdAsync(id);
            if (existingPlayerLevel == null)
                return NotFound();
            var deleted = await _iplayers.DeleteAsync(id, existingPlayerLevel);
            if (!deleted)
                return StatusCode(500, "An error occurred while deleting the PlayersLevel");
            return NoContent();
        }
    }
}
