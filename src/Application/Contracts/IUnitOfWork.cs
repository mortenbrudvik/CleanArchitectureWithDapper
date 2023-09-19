using Domain;

namespace Application.Contracts;

public interface IUnitOfWork : IDisposable
{
    IRepository<TaskItem> Tasks { get; }
    Task SaveAsync();
}