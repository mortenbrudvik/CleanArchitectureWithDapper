using Domain;
using SharedKernel;

namespace Application.Contracts;

public interface IRepository<T> where T : IEntity
{
    Task<IEnumerable<T>> Query(ISpecification<T> specification);
    Task<T> AddAsync(T entity);
}