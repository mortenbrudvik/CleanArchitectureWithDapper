using Application.Contracts;
using Infrastructure;
using Microsoft.Data.Sqlite;
using Xunit.Abstractions;

namespace Application.IntegrationTests;

public class TestBase : XunitContextBase
{
    private readonly SqliteDatabase _database;
    private readonly UnitOfWork _unitOfWork;

    public TestBase(ITestOutputHelper output) : base(output)
    {
        const string connectionString = "Data Source=InMemoryDb;Mode=Memory;Cache=Shared";
        
        _database = new SqliteDatabase(connectionString);
        _database.CreateDatabase();
        _unitOfWork = new UnitOfWork(new SqliteConnection(connectionString));
    }

    public IUnitOfWork UnitOfWork => _unitOfWork;

    public override void Dispose()
    {
        _unitOfWork.Dispose();
        _database.Dispose();
        
        base.Dispose();
    }
}

