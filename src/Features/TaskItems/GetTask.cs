using Application.Contracts;
using MediatR;

namespace Features.TaskItems;

public class GetTaskQuery : IRequest<TaskItemDto>
{
    public Guid Id { get; set; }
}

public class GetTaskQueryHandler : IRequestHandler<GetTaskQuery, TaskItemDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetTaskQueryHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<TaskItemDto> Handle(GetTaskQuery query, CancellationToken cancellationToken)
    {
        var task = await _unitOfWork.Tasks.GetById(query.Id);

        return new TaskItemDto
        {
            Id = task!.Id,
            Title = task.Title,
            Done = task.Done
        };
    }
}