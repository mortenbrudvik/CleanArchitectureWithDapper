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
    
    public async Task<ICollection<TaskItem>> Query(ISpecification<TaskItem> specification)
    {
        var query = _queryFactory.Query(nameof(TaskItem) + "s");
        query = specification.Apply(query);
        
        var sqlResult = _queryFactory.Compiler.Compile(query);
        var tasks =  await _connection.QueryAsync<TaskItem>(sqlResult.Sql, sqlResult.NamedBindings);
        return tasks.ToList();
    }

    public async Task<TaskItem> Add(TaskItem entity)
    {
        await _connection.InsertAsync(entity);
        
        return entity;
    }

    public async Task<ICollection<TaskItem>> GetAll()
    {
        var tasks = await _connection.GetAllAsync<TaskItem>();
        return tasks.ToList();
    }
}