using Application.Contracts;
using Infrastructure;
using Microsoft.Data.Sqlite;
using Xunit.Abstractions;

namespace Application.IntegrationTests;

public class UnitOfWorkFixture : XunitContextBase
{
    private readonly SqliteDatabase _database;
    private readonly UnitOfWork _unitOfWork;

    public UnitOfWorkFixture(ITestOutputHelper output) : base(output)
    {
        const string connectionString = "Data Source=TestMemoryDb;Mode=Memory;Cache=Shared";
        
        _database = new SqliteDatabase(connectionString);
        _database.Initialize();
        _unitOfWork = new UnitOfWork(new SqliteConnection(connectionString));
    }

    public void Insert(string sql)
    {
        _database.Insert(sql);
    }
    public IUnitOfWork UnitOfWork => _unitOfWork;

    public override void Dispose()
    {
        _unitOfWork.Dispose();
        _database.Dispose();
        
        base.Dispose();
    }
}

