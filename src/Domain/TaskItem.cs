using SharedKernel;

namespace Domain;

public class TaskItem : Entity<Guid>, IAggregateRoot
{
     public string Title { get; set; } = "";
     public bool Done { get; set; }
}