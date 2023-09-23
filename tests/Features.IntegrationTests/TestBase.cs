using Application.Contracts;
using Infrastructure;
using Microsoft.Data.Sqlite;
using Respawn;
using Respawn.Graph;

namespace Features.IntegrationTests;

public class TestBase : XunitContextBase
{
    private readonly UnitOfWork _unitOfWork;
    
    private readonly string _connectionString;


    public TestBase(ITestOutputHelper output) : base(output)
    {
        
        File.Delete("TestPlanner.sqlite");


        _connectionString = "Data Source=TestPlanner.sqlite";
        MigrationRunner.Run(_connectionString); // make sure database is up to date
        
        
        _unitOfWork = new UnitOfWork(new SqliteConnection(_connectionString));
    }

    public IUnitOfWork UnitOfWork => _unitOfWork;

    public override void Dispose()
    {
        _unitOfWork.Dispose();

        base.Dispose();
    }
}

