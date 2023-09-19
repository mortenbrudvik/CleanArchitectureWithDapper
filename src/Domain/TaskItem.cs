using SharedKernel;

namespace Domain;

public class TaskItem : Entity<Guid>, IAggregateRoot
{
     public string Title { get; set; } = "";
     public string? Description { get; set; }
     public bool Done { get; set; }
     public DateTime CreatedAt { get; set; } = DateTime.Now;
     public DateTime? UpdatedAt { get; set; }
     public DateTime? DeletedAt { get; set; }
}