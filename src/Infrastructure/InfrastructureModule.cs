using System.Data;
using Application.Contracts;
using Autofac;
using Domain;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace Infrastructure;

public class InfrastructureModule : Module
{
    private readonly IConfiguration _configuration;

    public InfrastructureModule(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    protected override void Load(ContainerBuilder builder)
    {
        var connectionString = _configuration.GetConnectionString("SqlLiteConnection");

        builder.Register(_ => new SqliteConnection(connectionString))
            .As<SqliteConnection>()
            .InstancePerLifetimeScope();

        builder.Register(c => c.Resolve<SqliteConnection>())
            .As<IDbConnection>()
            .InstancePerLifetimeScope();

        builder.RegisterType<TaskItemRepository>()
            .As<IRepository<TaskItem>>()
            .InstancePerLifetimeScope();

        builder.RegisterType<UnitOfWork>()
            .As<IUnitOfWork>()
            .InstancePerLifetimeScope();
    }
}