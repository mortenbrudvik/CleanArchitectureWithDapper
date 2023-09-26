using Ardalis.GuardClauses;
using SharedKernel;

using static SharedKernel.EntityValidation;

namespace Domain;

public class TaskItem : Entity<Guid>, IAggregateRoot
{
     private TaskItem() { } // Required by Dapper
     
     public TaskItem(string title)
     {
          Title = Guard.Against.NullOrEmptyOrMaxLength(title, nameof(title), TitleMaxLength);
     }
     
     public string Title { get; private set; }
     public bool Done { get; set; }
}