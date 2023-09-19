using System.Data;
using Application;
using Application.Contracts;
using Dapper;
using Domain;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace Infrastructure;

public class TaskRepository : IRepository<TaskItem>
{
    private readonly IDbConnection _connection;
    private readonly QueryFactory _queryFactory;

    public TaskRepository(IDbConnection connection)
    {
        _connection = connection;
        _queryFactory = new QueryFactory(connection, new SqlServerCompiler());
    }
    public IEnumerable<TaskItem> Query(ISpecification<TaskItem> specification)
    {
        var query = _queryFactory.Query(nameof(TaskItem) + "s");
        query = specification.Apply(query);
        
        var sqlResult = _queryFactory.Compiler.Compile(query);
        return _connection.Query<TaskItem>(sqlResult.Sql, sqlResult.NamedBindings);
    }
}