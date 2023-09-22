using Dapper.Contrib.Extensions;

namespace SharedKernel;

public abstract class Entity<T> : IEntity
{
    [ExplicitKey]
    public T Id { get; set; } = default!;
}