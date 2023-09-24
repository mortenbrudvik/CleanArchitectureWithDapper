using SharedKernel;

namespace Application.Contracts;

public interface IRepository<T> where T : IEntity
{
    Task<ICollection<T>> Query(ISpecification<T> specification);
    Task<T> Add(T entity);
    Task<ICollection<T>> GetAll();
}