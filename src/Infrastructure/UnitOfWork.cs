using Application.Contracts;
using Domain;
using Microsoft.Data.Sqlite;

namespace Infrastructure
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly Lazy<SqliteConnection> _connection;
        private readonly Lazy<IRepository<TaskItem>> _taskRepository;
        private SqliteTransaction _transaction = default!;

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

        public async Task Save(CancellationToken cancellationToken)
        {
            if (!_connection.IsValueCreated)
                return;

            try
            {
                await _transaction.CommitAsync(cancellationToken);
            }
            catch (Exception e)
            {
                await _transaction.RollbackAsync(cancellationToken);
                throw new Exception("Failed to commit changes to database", e);
            }
            finally
            {
                await _transaction.DisposeAsync();
                _transaction = (SqliteTransaction)await _connection.Value.BeginTransactionAsync(cancellationToken);
            }
        }

        public void Dispose()
        {
            if (!_connection.IsValueCreated) return;
            
            _transaction.Dispose();
            _connection.Value.Dispose();
        }
    }
}