using Application.Contracts;
using Dapper;
using Infrastructure;
using Infrastructure.Dapper;
using Microsoft.Data.Sqlite;

namespace Features.IntegrationTests;

public class UnitOfWorkTestBase : XunitContextBase
{
    private readonly UnitOfWork _unitOfWork;

    protected UnitOfWorkTestBase(ITestOutputHelper output) : base(output)
    {
        File.Delete("TestPlanner.sqlite");

        var connectionString = "Data Source=TestPlanner.sqlite";
        MigrationExt.MigrateUp(connectionString); // make sure database is up to date
        
        SqlMapper.AddTypeHandler(typeof(Guid), new GuidTypeHandler());
        
        _unitOfWork = new UnitOfWork(new SqliteConnection(connectionString));
    }

    protected IUnitOfWork UnitOfWork => _unitOfWork;

    public override void Dispose()
    {
        _unitOfWork.Dispose();

        base.Dispose();
    }
}

