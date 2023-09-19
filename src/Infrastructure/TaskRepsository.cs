using System.Data;
using Application;
using Dapper;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace Infrastructure;

public class TaskRepository : IRepository<Task>
{
    private readonly IDbConnection _connection;
    private readonly QueryFactory _queryFactory;

    public TaskRepository(IDbConnection connection)
    {
        _connection = connection;
        _queryFactory = new QueryFactory(connection, new SqlServerCompiler());
    }
    public IEnumerable<Task> Query(ISpecification<Task> specification)
    {
        var query = _queryFactory.Query(nameof(Task) + "s");
        query = specification.Apply(query);
        
        var sqlResult = _queryFactory.Compiler.Compile(query);
        return _connection.Query<Task>(sqlResult.Sql, sqlResult.NamedBindings);
    }
}