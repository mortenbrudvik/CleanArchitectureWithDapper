using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.Sqlite;

namespace Infrastructure
{
    internal class TaskRepository : ITaskRepository
    {
        private readonly SqliteConnection _connection;

        public TaskRepository(SqliteConnection connection)
        {
            _connection = connection;
        }

        public async Task<TaskItem> GetAsync(int id)
        {
            return await _connection.GetAsync<TaskItem>(id);
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            return await _connection.GetAllAsync<TaskItem>();
        }

        public async Task AddAsync(TaskItem taskItem)
        {
            taskItem.Id = await _connection.ExecuteScalarAsync<long>("INSERT INTO TaskItems (Name, IsCompleted) VALUES (@Name, @IsCompleted);SELECT last_insert_rowid();", taskItem);
        }

        public async Task UpdateAsync(TaskItem entity)
        {
            await _connection.UpdateAsync(entity);
        }

        public async Task DeleteAsync(TaskItem entity)
        {
            await _connection.DeleteAsync(entity);
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}