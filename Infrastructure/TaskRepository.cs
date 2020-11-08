using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Dapper;
using Microsoft.Data.Sqlite;

namespace Infrastructure
{
    public class TaskRepository : ITaskRepository, IDisposable
    {
        private readonly SqliteConnection _connection;

        public TaskRepository(SqliteConnection connection)
        {
            _connection = connection;
        }

        public async Task<TaskItem> CreateAsync(TaskItem taskItem)
        {
            taskItem.Id = await _connection.ExecuteScalarAsync<long>("INSERT INTO TaskItems (Name, IsCompleted)" +
                                                                    "VALUES (@Name, @IsCompleted);SELECT last_insert_rowid();", taskItem);
            return taskItem;
        }

        public async Task<IEnumerable<TaskItem>> GetAll() 
        {
            return await _connection.QueryAsync<TaskItem>("SELECT * FROM TaskItems");
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}