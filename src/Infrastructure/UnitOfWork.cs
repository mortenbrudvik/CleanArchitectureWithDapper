using System.Data;
using System.Data.Common;
using Application.Contracts;
using Domain;
using Microsoft.Data.Sqlite;

namespace Infrastructure
{
    internal class UnitOfWork: IUnitOfWork
    {
        private readonly Lazy<SqliteConnection> _connection;
        private readonly Lazy<IRepository<TaskItem>> _taskRepository;
        private SqliteTransaction _transaction;

        public UnitOfWork(SqliteConnection connection)
        {
            _connection = new Lazy<SqliteConnection>(() =>
            {
                connection.Open();
                _transaction = connection.BeginTransaction();
                return connection;
            });
            _taskRepository = new Lazy<IRepository<TaskItem>>(() => new TaskItemRepository(_connection.Value));
        }

        public IRepository<TaskItem> Tasks => _taskRepository.Value;

        public async Task SaveAsync(CancellationToken cancellationToken)
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
            _transaction.Dispose();
            if(_connection.IsValueCreated)
                _connection.Value.Dispose();
        }
    }
}