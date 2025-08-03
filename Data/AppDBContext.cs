using Microsoft.EntityFrameworkCore;
using RepositoryPattern_And_UnitOfWork.Models;

namespace RepositoryPattern_And_UnitOfWork.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options)
            : base(options)
        {
        }
        public DbSet<PlayersLevel> PlayersLevels { get; set; }


    }
}