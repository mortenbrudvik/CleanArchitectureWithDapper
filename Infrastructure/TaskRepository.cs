using System.Collections.Generic;
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
        public void Create(TaskItem taskItem)
        {
            using var connection = new SqliteConnection(_connectionString);

            connection.Execute("INSERT INTO TaskItems (Name, IsCompleted)" +
                                    "VALUES (@Name, @IsCompleted);", taskItem);
        }

        public IEnumerable<TaskItem> GetAll() 
        {
            using var connection = new SqliteConnection(_connectionString);

            var items = connection.Query<TaskItem>("SELECT * FROM TaskItems");

            return items;
        } 
    }
}