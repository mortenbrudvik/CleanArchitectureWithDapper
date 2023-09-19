using System.Data;
using Application.Contracts;
using Dapper;
using Dapper.Contrib.Extensions;
using Domain;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace Infrastructure;

public class TaskItemRepository : IRepository<TaskItem>
{
    private readonly IDbConnection _connection;
    private readonly QueryFactory _queryFactory;

    public TaskItemRepository(IDbConnection connection)
    {
        _connection = connection;
        _queryFactory = new QueryFactory(connection, new SqlServerCompiler());
    }
    
    public async Task<IEnumerable<TaskItem>> Query(ISpecification<TaskItem> specification)
    {
        var query = _queryFactory.Query(nameof(TaskItem) + "s");
        query = specification.Apply(query);
        
        var sqlResult = _queryFactory.Compiler.Compile(query);
        return await _connection.QueryAsync<TaskItem>(sqlResult.Sql, sqlResult.NamedBindings);
    }

    public async Task<TaskItem> Add(TaskItem entity)
    {
        var id = await _connection.InsertAsync(entity);
        entity.Id = id;
        
        return entity;
    }

    public async Task<IEnumerable<TaskItem>> GetAll()
    {
        return await _connection.GetAllAsync<TaskItem>();
    }
}