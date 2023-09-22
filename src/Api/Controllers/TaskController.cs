using Features.TaskItems;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
 
[ApiController]
[Route("[controller]")]
public class TaskController : ControllerBase
{
    private readonly IMediator _mediator;

    public TaskController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<ActionResult<TaskItemDto>> Create(CreateTaskCommand command)
    {
        return await _mediator.Send(command);
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskItemDto>>> GetAll(GetTasksQuery query)
    {
        return await _mediator.Send(query);
    }

}