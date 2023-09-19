namespace Application.Features.TaskItems;

public class TaskItemDto
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public bool Done { get; set; }
}