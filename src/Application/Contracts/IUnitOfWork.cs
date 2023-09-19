using Domain;

namespace Application.Contracts;

public interface IUnitOfWork : IDisposable
{
    IRepository<TaskItem> Tasks { get; }
    Task Save(CancellationToken cancellationToken);
}