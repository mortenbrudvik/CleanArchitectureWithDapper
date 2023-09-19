using Application.Contracts;
using Domain;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("SqlLiteConnection");
        
        services.AddScoped<IRepository<TaskItem>>(_ => new TaskRepository(new SqliteConnection(connectionString)));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        return services;
    }
    
}