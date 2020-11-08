using System;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using Microsoft.Data.Sqlite;

namespace Infrastructure
{
    internal class UnitOfWork: IUnitOfWork
    {
        private readonly SqliteConnection _connection;
        private readonly SqliteTransaction _transaction;

        public UnitOfWork(SqliteConnection connection)
        {
            _connection = connection;
            _connection.Open();
            _transaction =_connection.BeginTransaction();
            TaskRepository = new TaskRepository(_connection);
        }

        public ITaskRepository TaskRepository { get; }

        public async Task SaveAsync()
        {
            try
            {
                await _transaction.CommitAsync();
            }
            catch (Exception e)
            {
                await _transaction.RollbackAsync();
                throw new Exception("Failed to commit changes to database", e);
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _connection?.Dispose();
        }
    }
}