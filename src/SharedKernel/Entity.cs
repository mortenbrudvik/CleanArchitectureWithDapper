namespace SharedKernel;

public abstract class Entity<T> : IEntity
{
    public T Id { get; set; } = default!;
}