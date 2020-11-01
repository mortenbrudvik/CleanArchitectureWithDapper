using Dapper;
using Microsoft.Data.Sqlite;

namespace SQLiteDapperConsole
{
    public class TaskRepository
    {
        private readonly string _connectionString;

        public TaskRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void Create(TaskItem taskItem)
        {
            using var connection = new SqliteConnection(_connectionString);

            connection.ExecuteAsync("INSERT INTO TaskItems (Name, IsCompleted)" +
                                    "VALUES (@Name, @IsCompleted);", taskItem);
        }
    }
}