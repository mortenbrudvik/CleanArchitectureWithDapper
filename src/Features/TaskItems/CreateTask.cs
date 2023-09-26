using Application.Contracts;
using Domain;
using MediatR;

namespace Features.TaskItems;

public class CreateTaskCommand : IRequest<TaskItemDto>
{
    public required string Title { get; set; } 
}

public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, TaskItemDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateTaskCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<TaskItemDto> Handle(CreateTaskCommand command, CancellationToken cancellationToken)
    {
        var task = new TaskItem(command.Title)
        {
            Id = Guid.NewGuid()
        };

        await _unitOfWork.Tasks.Add(task);
        await _unitOfWork.Save(cancellationToken);

        return new TaskItemDto
        {
            Id = task.Id,
            Title = task.Title,
            Done = task.Done
        };
    }
}
