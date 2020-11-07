using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;

namespace Infrastructure
{
    public class TaskRepository : ITaskRepository
    {
        private readonly string _connectionString;

        public TaskRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<TaskItem> CreateAsync(TaskItem taskItem)
        {
            await using var connection = new SqliteConnection(_connectionString);

            taskItem.Id = await connection.ExecuteScalarAsync<long>("INSERT INTO TaskItems (Name, IsCompleted)" +
                                                                    "VALUES (@Name, @IsCompleted);SELECT last_insert_rowid();", taskItem);
            return taskItem;
        }

        public async Task<IEnumerable<TaskItem>> GetAll() 
        {
            await using var connection = new SqliteConnection(_connectionString);

            return await connection.QueryAsync<TaskItem>("SELECT * FROM TaskItems");
        } 
    }
}