 using DataAccessLayer;
using DataAccessLayer.Repositories;
using DataAccessLayer.Seeding;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Bootstrap
{
    public static class ServiceExtensions
    {
        public static void ConfigurePortfolioContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["ConnectionStrings:PortfolioConnection"];
            services.AddDbContext<PortfolioContext>(options =>
                options.UseSqlServer(connectionString));
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
