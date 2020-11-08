using ApplicationCore.Interfaces;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddScoped<ITaskRepository>(_ => new TaskRepository(new SqliteConnection(connectionString)));
            services.AddScoped<IUnitOfWork>(_ => new UnitOfWork(new SqliteConnection(connectionString)));

            return services;
        }
    }
}