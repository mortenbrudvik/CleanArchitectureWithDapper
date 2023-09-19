using Dapper;
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

    public void CreateDatabase()
    {
        _connection.Execute(
            """
            CREATE TABLE TaskItems (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Title TEXT NOT NULL DEFAULT '',
                Done INTEGER NOT NULL DEFAULT 0
            )
            """
        );
    }


    public void Dispose()
    {
        _connection.Dispose();
    }
}