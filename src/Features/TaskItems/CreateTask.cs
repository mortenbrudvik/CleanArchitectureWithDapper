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

    public async Task<TaskItemDto> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = new TaskItem
        {
            Title = request.Title
        };

        await _unitOfWork.Tasks.Add(new TaskItem
        {
            Title = request.Title
        });
        await _unitOfWork.Save(cancellationToken);

        return new TaskItemDto
        {
            Id = task.Id,
            Title = task.Title,
            Done = task.Done
        };
    }
}
