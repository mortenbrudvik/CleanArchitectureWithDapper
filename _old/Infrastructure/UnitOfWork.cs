using System;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using Microsoft.Data.Sqlite;

namespace Infrastructure
{
    internal class UnitOfWork: IUnitOfWork
    {
        private readonly Lazy<SqliteConnection> _connection;
        private readonly Lazy<ITaskRepository> _taskRepository;
        private SqliteTransaction _transaction;

        public UnitOfWork(string connectionString)
        {
            _connection = new Lazy<SqliteConnection>(() =>
            {
                var connection = new SqliteConnection(connectionString);
                connection.Open();
                _transaction = connection.BeginTransaction();
                return connection;
            });
            _taskRepository = new Lazy<ITaskRepository>(() => new TaskRepository(_connection.Value));
        }

        public ITaskRepository TaskRepository => _taskRepository.Value;

        public async Task SaveAsync()
        {
            if (!_connection.IsValueCreated)
                return;

            try
            {
                await _transaction.CommitAsync();
            }
            catch (Exception e)
            {
                await _transaction.RollbackAsync();
                throw new Exception("Failed to commit changes to database", e);
            }
            finally
            {
                await _transaction.DisposeAsync();
                _transaction = (SqliteTransaction)await _connection.Value.BeginTransactionAsync();
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            if(_connection.IsValueCreated)
                _connection.Value.Dispose();
        }
    }
}