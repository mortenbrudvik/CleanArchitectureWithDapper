using Application.Contracts;
using MediatR;

namespace Features.TaskItems;

public class GetTasksCommand : IRequest<TaskItemDto[]>
{
}

public class GetTasksCommandHandler : IRequestHandler<GetTasksCommand, TaskItemDto[]>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetTasksCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<TaskItemDto[]> Handle(GetTasksCommand request, CancellationToken cancellationToken)
    {
        var tasks = await _unitOfWork.Tasks.GetAll();
        return tasks.Select(task => new TaskItemDto
        {
            Id = task.Id,
            Title = task.Title,
            Done = task.Done
        }).ToArray();
    }
}