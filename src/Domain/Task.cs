namespace Domain;

public class Task
{
     public int Id { get; set; }
     public string Title { get; set; } = "";
     public string? Description { get; set; }
     public bool Done { get; set; }
     public DateTime CreatedAt { get; set; } = DateTime.Now;
     public DateTime? UpdatedAt { get; set; }
     public DateTime? DeletedAt { get; set; }
}