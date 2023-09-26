using Application.Contracts;
using Dapper;
using Infrastructure;
using Infrastructure.Dapper;
using Microsoft.Data.Sqlite;

namespace Features.IntegrationTests;

public class UnitOfWorkTestBase : XunitContextBase
{
    private readonly UnitOfWork _unitOfWork;
    private readonly string _connectionString;

    public UnitOfWorkTestBase(ITestOutputHelper output) : base(output)
    {
        File.Delete("TestPlanner.sqlite");

        _connectionString = "Data Source=TestPlanner.sqlite";
        MigrationExt.MigrateUp(_connectionString); // make sure database is up to date
        
        SqlMapper.AddTypeHandler(typeof(Guid), new GuidTypeHandler());
        
        _unitOfWork = new UnitOfWork(new SqliteConnection(_connectionString));
    }

    public IUnitOfWork UnitOfWork => _unitOfWork;

    public override void Dispose()
    {
        _unitOfWork.Dispose();

        base.Dispose();
    }
}

