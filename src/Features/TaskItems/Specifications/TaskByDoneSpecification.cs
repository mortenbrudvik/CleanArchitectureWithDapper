using Application.Contracts;
using SqlKata;

namespace Features.TaskItems.Specifications;

public class TaskByStatusSpecification : ISpecification<Task>
{
    private readonly bool _done;

    public TaskByStatusSpecification(bool done) => _done = done;

    public Query Apply(Query query) =>
        query.Where(new { Done = _done });
}