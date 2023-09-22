using FluentMigrator.Runner;
using Infrastructure.Migrations;
using Microsoft.Extensions.Logging;

namespace Infrastructure;
using Microsoft.Extensions.DependencyInjection;

public static class MigrationRunner
{
    public static void Run(string connectionString)
    {
        var serviceProvider =  new ServiceCollection()
            .AddLogging(lb => lb.AddDebug().AddFluentMigratorConsole())
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddSQLite()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(typeof(AddTaskTable).Assembly).For.Migrations())
            //.AddLogging(lb => lb.AddFluentMigratorConsole())
            .BuildServiceProvider(false);
        
        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateUp();
    }
}