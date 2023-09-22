using Dapper;
using Infrastructure.Dapper;
using Microsoft.Data.Sqlite;

namespace Application.IntegrationTests;

public class SqliteDatabase : IDisposable
{
    private readonly SqliteConnection _connection;

    public SqliteDatabase(string connectionString)
    {
        _connection = new SqliteConnection(connectionString);
        _connection.Open();
    }

    public virtual void Initialize()
    {
        Insert("DROP TABLE IF EXISTS Cars");
        
        Insert("DROP TABLE IF EXISTS TaskItems");
        Insert(    
            """
               CREATE TABLE TaskItems (
                   Id INTEGER PRIMARY KEY AUTOINCREMENT,
                   Title TEXT NOT NULL DEFAULT '',
                   Done INTEGER NOT NULL DEFAULT 0
               )
           """);
        
        Insert(    
            """
                CREATE TABLE Cars (
                    Id UNIQUEIDENTIFIER PRIMARY KEY,
                    Name TEXT NOT NULL,
                    Year INTEGER NOT NULL
                )
            """);
        SqlMapper.AddTypeHandler(typeof(Guid), new GuidTypeHandler());

        var car = new Car
        {
            Id = Guid.NewGuid(),
            Name = "Ford",
            Year = 2021
        };  
        
        var id = _connection.Execute("INSERT INTO Cars (Id, Name, Year) VALUES (@Id, @Name, @Year)", car);

        var cars = _connection.Query<Car>("SELECT * FROM Cars");

        
    }

    public void Insert(string sql) => _connection.Execute(sql);
    public void Insert<T>(string sql, T entity ) => _connection.Execute(sql, entity);
    public IEnumerable<T> Get<T>(string sql ) => _connection.Query<T>(sql);


    public void Dispose() => _connection.Dispose();
}
