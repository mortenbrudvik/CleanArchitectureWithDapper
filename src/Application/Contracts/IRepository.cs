using SharedKernel;

namespace Application.Contracts;

public interface IRepository<T> where T : IEntity
{
    IEnumerable<T> Query(ISpecification<T> specification);
}