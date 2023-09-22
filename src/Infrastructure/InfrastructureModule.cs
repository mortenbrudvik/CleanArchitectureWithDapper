using System.Data;
using Application.Contracts;
using Autofac;
using Dapper;
using Domain;
using Infrastructure.Dapper;
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
        // Repositories and Unit of Work
        var connectionString = _configuration.GetConnectionString("SqlLiteConnection");
        
        SqlMapper.AddTypeHandler(typeof(Guid), new GuidTypeHandler());

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
        
        // Migrations
        MigrationRunner.Run(connectionString!);
    }
}