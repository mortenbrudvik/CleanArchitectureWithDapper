using SqlKata;

namespace Application.Contracts;

public interface ISpecification<T>
{
    Query Apply(Query query);
}