using DataAccessLayer.Configurations;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class PortfolioContext : DbContext
    {
        public PortfolioContext(DbContextOptions options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }

        public virtual DbSet<User> User { get; set; } = null!;
    }
}