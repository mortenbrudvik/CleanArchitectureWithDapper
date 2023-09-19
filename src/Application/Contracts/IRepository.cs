using SharedKernel;

namespace Application.Contracts;

public interface IRepository<T> where T : IEntity
{
    Task<IEnumerable<T>> Query(ISpecification<T> specification);
    Task<T> Add(T entity);
    Task<IEnumerable<T>> GetAll();
}