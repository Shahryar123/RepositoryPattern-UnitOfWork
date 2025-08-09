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
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure entity relationships and properties if needed
            modelBuilder.Entity<PlayersLevel>().ToTable("PlayersLevels");
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Order>().ToTable("Orders");
            // Example of configuring a one-to-many relationship
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Orders)
                .WithOne(o => o.Product)
                .HasForeignKey(o => o.ProductId);
        }


    }
}