using SqlKata;

namespace Application;

public interface ISpecification<T>
{
    Query Apply(Query query);
}