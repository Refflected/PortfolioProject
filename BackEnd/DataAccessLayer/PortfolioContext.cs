using DataAccessLayer.Configurations;
using Domain.Entities;
using Domain.Entities.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class PortfolioContext : DbContext
    {
        public PortfolioContext(DbContextOptions options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
        }

        public virtual DbSet<User> User { get; set; } = null!;

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override async Task<int> SaveChangesAsync(
           bool acceptAllChangesOnSuccess,
           CancellationToken cancellationToken = default
        )
        {
            OnBeforeSaving();
            return (await base.SaveChangesAsync(acceptAllChangesOnSuccess,
                          cancellationToken));
        }

        private void OnBeforeSaving()
        {
            var entries = ChangeTracker.Entries();
            var utcNow = DateTime.UtcNow;

            foreach (var entry in entries)
            {
                // for entities that inherit from BaseEntity,
                // set UpdatedDate / CreatedDate appropriately
                if (entry.Entity is BaseEntity trackable)
                {
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            // set the updated date to "now"
                            trackable.UpdatedDate = utcNow;

                            // mark property as "don't touch"
                            // we don't want to update on a Modify operation
                            entry.Property("CreatedOn").IsModified = false;
                            break;

                        case EntityState.Added:
                            // set both updated and created date to "now"
                            trackable.CreatedDate = utcNow;
                            trackable.UpdatedDate = utcNow;
                            break;
                    }
                }
            }
        }
    }
}