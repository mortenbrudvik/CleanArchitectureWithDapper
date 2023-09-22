using Application.Contracts;
using MediatR;

namespace Features.TaskItems;

public class GetTasksQuery : IRequest<TaskItemDto[]>
{
}

public class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, TaskItemDto[]>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetTasksQueryHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<TaskItemDto[]> Handle(GetTasksQuery request, CancellationToken cancellationToken)
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