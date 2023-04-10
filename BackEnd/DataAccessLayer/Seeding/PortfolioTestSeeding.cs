using Domain.Entities;
using Domain.Entities.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer.Seeding
{
    public class PortfolioTestSeeding
    {
        public static void SeedDatabase(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<PortfolioContext>();

            ClearTable<User>(context);
            SeedUsers(context);
        }

        private static void ClearTable<TEntity>(PortfolioContext context) where TEntity : BaseEntity
        {
            RemoveAllEntities<TEntity>(context);
            context.SaveChanges();
        }

        private static void RemoveAllEntities<TEntity>(PortfolioContext context) where TEntity: BaseEntity
        {
            context.RemoveRange(context.Set<TEntity>());
        }
        private static void SeedUsers(PortfolioContext context)
        {
            context.Database.EnsureCreated();

            var users = new List<User>
            {
                new User
                {
                    UserName ="Refflected",
                    Password = "azerty123",
                    Email = "andy-klinkers@hotmail.com",
                    FirstName = "Andy",
                    LastName = "Klinkers",
                },
                new User
                {
                    UserName ="Dummy",
                    Password = "azerty123",
                    Email = "dummy@hotmail.com",
                    FirstName = "Dummy",
                    LastName = "Dummy",
                },
                new User
                {
                    UserName ="admin",
                    Password = "admin",
                    Email = "admin@hotmail.com",
                },
            };

            context.User.AddRange(users);
            context.SaveChanges();
        }
    }
}
