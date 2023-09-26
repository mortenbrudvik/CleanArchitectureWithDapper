using FluentMigrator.Runner;
using Infrastructure.Migrations;
using Microsoft.Extensions.Logging;

namespace Infrastructure;
using Microsoft.Extensions.DependencyInjection;

public static class MigrationExt
{
    public static void MigrateUp(string connectionString)
    {
        var serviceProvider =  new ServiceCollection()
            .AddLogging(lb => lb.AddDebug().AddFluentMigratorConsole())
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddSQLite()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(typeof(AddTaskTable).Assembly).For.Migrations())
            .BuildServiceProvider(false);
        
        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateUp();
    }
}