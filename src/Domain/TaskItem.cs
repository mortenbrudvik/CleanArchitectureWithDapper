using SharedKernel;

namespace Domain;

public class TaskItem : Entity<int>, IAggregateRoot
{
     public string Title { get; set; } = "";
     public bool Done { get; set; }
}