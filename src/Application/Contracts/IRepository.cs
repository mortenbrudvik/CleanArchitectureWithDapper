namespace Application;

public interface IRepository<T>
{
    IEnumerable<T> Query(ISpecification<T> specification);
}