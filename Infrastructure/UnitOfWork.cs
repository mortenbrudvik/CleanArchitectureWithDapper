using System;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using Microsoft.Data.Sqlite;

namespace Infrastructure
{
    internal class UnitOfWork: IUnitOfWork
    {
        private readonly string _connectionString;
        private SqliteConnection _connection;
        private SqliteTransaction _transaction;
        private  ITaskRepository _taskRepository;

        public UnitOfWork(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqliteConnection Connection
        {
            get
            {
                if (_connection != null) return _connection;
                
                _connection = new SqliteConnection(_connectionString);
                _connection.Open();
                _transaction = _connection.BeginTransaction();

                return _connection;
            }
        }

        public ITaskRepository TaskRepository
        {
            get
            {
                _taskRepository ??= new TaskRepository(Connection);
                return _taskRepository;
            }
        }

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