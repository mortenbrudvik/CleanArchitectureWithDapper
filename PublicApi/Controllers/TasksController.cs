using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PublicApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ILogger<TasksController> _logger;
        private readonly ITaskRepository _taskRepository;

        public TasksController(ILogger<TasksController> logger, ITaskRepository taskRepository)
        {
            _logger = logger;
            _taskRepository = taskRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<TaskItem>> Get()
        {
            return await _taskRepository.GetAllAsync();
        }

        [HttpPost]
        public async Task<TaskItem> PostTask([FromServices] IUnitOfWork unitOfWork, TaskItem task)
        {
            await unitOfWork.TaskRepository.AddAsync(task);

            await unitOfWork.SaveAsync();

            return task;
        }
    }
}
