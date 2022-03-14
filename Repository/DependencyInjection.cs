using Application.Configurations;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Services.Implementation;
using Services.Jobs;

namespace Repository
{
    public static class DependencyInjection
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = string.Empty;
            connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services.AddScoped<IUserService, UserService>();
            services.Configure<SchedulerConfigurations>(configuration.GetSection("SchedulerConfiguration"));
            services.AddTransient<JobFactory>();
            services.AddScoped<ClearMemoryJob>();
        }
    }
}
