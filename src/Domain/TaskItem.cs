using Ardalis.GuardClauses;
using SharedKernel;

namespace Domain;

public class TaskItem : Entity<Guid>, IAggregateRoot
{
     public TaskItem(string title)
     {
          Title = Guard.Against.NullOrEmpty(title, nameof(title));
     }
     
     public string Title { get; private set; }
     public bool Done { get; set; }
}