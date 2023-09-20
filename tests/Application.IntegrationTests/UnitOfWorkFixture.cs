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
        const string connectionString = "Data Source=:memory:;Mode=Memory;Cache=Shared";
        
        _database = new SqliteDatabase(connectionString);
        _database.Initialize();
        _unitOfWork = new UnitOfWork(new SqliteConnection(connectionString));
    }

    public void Insert(string sql) => _database.Insert(sql);
    public void Insert<T>(string sql, T entity) => _database.Insert(sql, entity);
    public IEnumerable<T> GetAll<T>(string sql) => _database.Get<T>(sql);
    public IUnitOfWork UnitOfWork => _unitOfWork;

    public override void Dispose()
    {
        _unitOfWork.Dispose();
        
        base.Dispose();
    }
}

